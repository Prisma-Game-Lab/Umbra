using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookChain : MonoBehaviour
{
    public GameObject ChainPrefab;
    public GameObject Target;
	private List<GameObject> chainNodes;
	public float ChainNodeSize;
	[Tooltip("Desmarque para não mostrar a corrente")]public bool ShouldShowChain = true;

    private Vector3 _targetPos;

    private void Start()
    {
        // _targetPos = Target.transform.position;
		chainNodes = new List<GameObject>();
    }

    private void FixedUpdate()
    {
		if (ShouldShowChain) {
			foreach (GameObject g in chainNodes) {
				Destroy(g);
			}


			_targetPos = Target.transform.position;

			// Rotates the particle
			Vector3 diff = _targetPos - PlayerPhysics.GetInstance ().transform.position;
			diff.Normalize ();
			float rot_z = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;

			//Distância
			Vector2 distanceVec = new Vector2 (transform.position.x - PlayerPhysics.GetInstance ().transform.position.x,
				                     transform.position.y - PlayerPhysics.GetInstance ().transform.position.y);
			float distanceNum = distanceVec.magnitude;

			//spawna chains
			//Debug.Log (distanceNum + " " + ChainNodeSize);
			Vector3 nextSpawnPos = transform.position;
			if (rot_z >= 90) {
				for (float i = ChainNodeSize / 2; i < distanceNum - 4 * ChainNodeSize; i += ChainNodeSize) {
					nextSpawnPos = transform.position - new Vector3 (distanceVec.x * i, distanceVec.y * i, 0);

					if (nextSpawnPos.x > PlayerPhysics.GetInstance().transform.position.x) {
						nextSpawnPos = new Vector3(PlayerPhysics.GetInstance().transform.position.x, nextSpawnPos.y, nextSpawnPos.z);
					}

					if (nextSpawnPos.y < PlayerPhysics.GetInstance().transform.position.y) {
						nextSpawnPos = new Vector3(nextSpawnPos.x, PlayerPhysics.GetInstance().transform.position.y, nextSpawnPos.z);
					}

					CreateNode (nextSpawnPos, 90 + rot_z);
				}
			} else {
				for (float i = ChainNodeSize / 2; i < distanceNum - 4 * ChainNodeSize; i += ChainNodeSize) {
					nextSpawnPos = transform.position - new Vector3 (distanceVec.x * i, distanceVec.y * i, 0);

					if (nextSpawnPos.x < PlayerPhysics.GetInstance().transform.position.x) {
						nextSpawnPos = new Vector3(PlayerPhysics.GetInstance().transform.position.x, nextSpawnPos.y, nextSpawnPos.z);
					}

					if (nextSpawnPos.y < PlayerPhysics.GetInstance().transform.position.y) {
						nextSpawnPos = new Vector3(nextSpawnPos.x, PlayerPhysics.GetInstance().transform.position.y, nextSpawnPos.z);
					}

					CreateNode (nextSpawnPos, 90 + rot_z);
				}
			}
		}
    }

	public void CreateNode(Vector3 position, float eulerAngle){
		chainNodes.Add (GameObject.Instantiate (ChainPrefab, position, Quaternion.Euler(0,0,eulerAngle), transform));
	}

}

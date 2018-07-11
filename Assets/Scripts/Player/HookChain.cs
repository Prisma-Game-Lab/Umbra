using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookChain : MonoBehaviour
{
    public GameObject ChainPrefab;
    public GameObject Target;
	private List<GameObject> chainNodes;
	public float ChainNodeSize;

    private Vector3 _targetPos;

    private void Start()
    {
        // _targetPos = Target.transform.position;
		chainNodes = new List<GameObject>();
    }

    private void FixedUpdate()
    {
		_targetPos = Target.transform.position;

		// Rotates the particle
		Vector3 diff = _targetPos - transform.position;
		diff.Normalize();
		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

		Vector3 distanceVec = _targetPos - PlayerPhysics.GetInstance().transform.position;
		float distanceNum = distanceVec.magnitude;

		for (float i = ChainNodeSize/2; i < distanceNum; i += ChainNodeSize) {
			CreateNode(i * distanceVec, rot_z);
		}

    }

	public void CreateNode(Vector3 position, float eulerAngle){
		chainNodes.Add (GameObject.Instantiate (ChainPrefab, position, Quaternion.Euler(0,0,eulerAngle), transform));
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

	[Header("Player Object")]
	public GameObject PlayerObject;

	[Header("Middle (Gameplay) Layer")]
	public GameObject MiddleLayerObject;
	public float MiddleLayerParallaxCoefficient = 0.05f;

	[Header("Front Layer")]
	public GameObject FrontLayerObject;
	public float FrontLayerParallaxCoefficient = 0.1f;

	private Vector3 _middleLayerInitialPosition;
	private Vector3 _frontLayerInitialPosition;
	private Vector2 _playerInitialPositionProjected;

	// Use this for initialization
	void Start () {
		if (this.PlayerObject == null ||
				this.MiddleLayerObject == null ||
				this.FrontLayerObject == null)
		{
			Debug.LogError("Error at Parallax Script. PlayerObject or MiddleLayerObject or FrontLayerObject were not assigned.");
			// UnityEditor.EditorApplication.isPlaying = false;
			return;
		}
		this._frontLayerInitialPosition = this.FrontLayerObject.transform.position;
		this._middleLayerInitialPosition = this.MiddleLayerObject.transform.position;
		this._playerInitialPositionProjected = new Vector2(	this.PlayerObject.transform.position.x,
																												this.PlayerObject.transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 playerPositionProjected = new Vector2(	this.PlayerObject.transform.position.x,
																										this.PlayerObject.transform.position.y);
		Vector2 delta = playerPositionProjected - this._playerInitialPositionProjected;
		this.FrontLayerObject.transform.position = this._frontLayerInitialPosition +
																							-1.0f * this.FrontLayerParallaxCoefficient * new Vector3(delta.x, delta.y, 0.0f);
		this.MiddleLayerObject.transform.position = this._middleLayerInitialPosition +
																							-1.0f * this.MiddleLayerParallaxCoefficient * new Vector3(delta.x, delta.y, 0.0f);
	}
}

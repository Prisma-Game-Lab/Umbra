using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallSingletonOnStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log(KeyBindings.Instance.playerMoveLeft);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

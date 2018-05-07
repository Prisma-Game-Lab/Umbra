using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPreloadInstancing : MonoBehaviour {

	void Start () {
		Debug.Log(TestSingleton.Instance.ping());
	}
	
	void Update () {
		
	}
}

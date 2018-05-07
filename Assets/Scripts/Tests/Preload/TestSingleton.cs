using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSingleton : Singleton<TestSingleton> {

	void Start () {
		
	}

	public bool ping() {
		return true;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstSceneSetter : MonoBehaviour {

	public string firstSceneName;

	void Start () {
		SceneManager.LoadScene(firstSceneName);
	}
	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

	public string firstScene;

	// Use this for initialization
	void Start () {
		SceneManager.LoadScene(this.firstScene);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

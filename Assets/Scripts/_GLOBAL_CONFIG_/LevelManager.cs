using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager> {

	public int PlayerLayer;
	public string MenuSceneName;
	public string[] SceneNames;

	private int currentLevel = -1;

	void Start () {
		// Finds the scene that is currently being played and set current level to it.
		string currentSceneName = SceneManager.GetActiveScene().name;
		for (int i = 0; i < this.SceneNames.Length; i++) {
			if(currentSceneName == this.SceneNames[i]) {
				// If found the scene, set the current level to it.
				this.currentLevel = i;
				break;
			}
		}
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyBindings.Instance.GameFlowReloadScene))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}

	public void GoToLevel(int level)
	{
		if(level < SceneNames.Length && level >= 0)
		{
			currentLevel = level;
			SceneManager.LoadScene(SceneNames[level]);
		}
		else if(level == -1)
		{
			currentLevel = -1;
			SceneManager.LoadScene(MenuSceneName);
		}
		else
		{
			Debug.LogError("LevelManager attempted to go to a level that does not exit. Level = " + level);
		}
	}

	public void GoToNextLevel()
	{
		if(currentLevel + 1 < SceneNames.Length)
		{
			GoToLevel(currentLevel + 1);
		}
		else
		{
			GoToLevel(-1);
		}
	}

	public void ResetLevel()
	{
		GoToLevel(currentLevel);
	}

}

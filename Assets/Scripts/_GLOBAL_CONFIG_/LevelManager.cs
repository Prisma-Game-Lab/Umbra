using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager> {

	public int PlayerLayer;
	public string MenuSceneName;
	public string[] SceneNames;

	private int currentLevel = -1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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

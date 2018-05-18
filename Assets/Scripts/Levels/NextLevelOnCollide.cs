using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelOnCollide : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D coll)
	{
		if(coll.gameObject.layer == LevelManager.Instance.PlayerLayer)
		{
			Debug.Log("Goal Reached! Changing Level...");
			LevelManager.Instance.GoToNextLevel();
		}
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	All key bindings in the game are defined here.

	Usage example:
	Input.GetKeyDown(KeyBindings.Instance.PlayerMoveLeft)
*/

public class KeyBindings : Singleton<KeyBindings> {

	[Header("Player Control")]
	public KeyCode PlayerMoveLeft;
	public KeyCode PlayerMoveRight;
	public KeyCode PlayerJump;
	public KeyCode PlayerHook;

	[Header("Game Flow")]
	public KeyCode GameFlowOpenMenu;
	public KeyCode GameFlowReloadScene;

	public int GetAxisX() {
		int delta = 0;
		if(Input.GetKeyDown(KeyBindings.Instance.PlayerMoveRight)) {
			delta += 1;
		}
		if(Input.GetKeyDown(KeyBindings.Instance.PlayerMoveLeft)) {
			delta -= 1;
		}
		return delta;
	}
	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBindings : Singleton<KeyBindings> {

	[Header("Player Control")]
	public KeyCode playerMoveUp;
	public KeyCode playerMoveLeft;
	public KeyCode playerMoveDown;
	public KeyCode playerMoveRight;
	public KeyCode playerJump;
	public KeyCode playerDash;
	public KeyCode playerHook;
	
	[Header("Menus")]
	public KeyCode menuMoveUp;
	public KeyCode menuMoveLeft;
	public KeyCode menuMoveDown;
	public KeyCode menuMoveRight;
	public KeyCode menuEnter;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

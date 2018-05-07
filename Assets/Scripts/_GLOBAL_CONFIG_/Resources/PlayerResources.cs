using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : Singleton<PlayerResources> {

	[Header("Player Resources")]
	private int playerCurrentHealth;
	public int playerMaxHealth;
	public int playerStartingHealth;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void forcePlayerCurrentHealthInValidRange()
	{
		this.playerCurrentHealth = Mathf.Max(0, this.playerCurrentHealth);
		this.playerCurrentHealth = Mathf.Min(this.playerMaxHealth, this.playerCurrentHealth);
	}

	public int getPlayerCurrentHealth()
	{
		return this.playerCurrentHealth;
	}

	public void setPlayerCurrentHealth(int newCurrentHealth)
	{
		this.playerCurrentHealth = newCurrentHealth;
		this.forcePlayerCurrentHealthInValidRange();
	}

	public void changePlayerCurrentHealthBy(int deltaHealth)
	{
		this.playerCurrentHealth += deltaHealth;
		this.forcePlayerCurrentHealthInValidRange();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceVolumeChanger : MonoBehaviour {
	public bool ShouldChangeVolume = false;
	[Range(0,1)] public float NewVolume;

	// Use this for initialization
	void Start () {
		if (ShouldChangeVolume) {
			LevelManager.Instance.ProgressiveAmbientSound.volume = NewVolume;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

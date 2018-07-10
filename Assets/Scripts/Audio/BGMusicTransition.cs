using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicTransition : MonoBehaviour {
	[Tooltip("Música de fundo da fase")] public AudioClip BGMusic;
	// Use this for initialization
	void Start () {
		if (LevelManager.Instance.BackgroundMusicSound.clip != BGMusic) {
			LevelManager.Instance.BackgroundMusicSound.Stop();
			LevelManager.Instance.BackgroundMusicSound.clip = BGMusic;
			LevelManager.Instance.BackgroundMusicSound.Play();
		}
	}
}

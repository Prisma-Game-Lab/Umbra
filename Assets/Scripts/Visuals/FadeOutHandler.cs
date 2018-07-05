using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutHandler : MonoBehaviour {
	public void OnFadeOutEnd() {
		LevelManager.Instance.OnFadeOutEnd(); //chama o próximo nível ao final do fadeout
	}
}

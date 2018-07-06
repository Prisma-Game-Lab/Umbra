using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogPanel : MonoBehaviour {
	public GameObject panel;
	public Text text;

	// Use this for initialization
	void Start () {
		panel.SetActive(false);	
	}
	
	public void SetActive(bool status){
		panel.SetActive(status);
	}

	public void SetMessage(string message){
		text.text = message;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour {
	public string[] Messages;
	private int curMessage = 0;
	private DialogPanel dialogPanel;
	private bool showingMsg = false;

	public void Start(){
		dialogPanel = GameObject.Find("Canvas").GetComponent<DialogPanel>();
		Debug.Log (dialogPanel);
	}

	public void Update(){
		if (showingMsg) {
			if (Input.GetKeyDown(KeyCode.Return)) {
				curMessage++;

				//Corrige indice caso necessário
				if (curMessage >= Messages.Length) {
					curMessage = 0;
				}

				//Muda mensagem na placa
				dialogPanel.SetMessage(Messages[curMessage]);
			}	
		}
	}

	public void OnTriggerEnter2D(Collider2D col){
		//checa se foi o player
		if (col.gameObject.tag != "Player")
			return;

		dialogPanel.SetMessage(Messages[curMessage]);
		dialogPanel.SetActive(true);

		showingMsg = true;
	}

	public void OnTriggerExit2D(Collider2D col){
		//checa se foi o player
		if (col.gameObject.tag != "Player")
			return;
		
		dialogPanel.SetActive(false);
		showingMsg = false;
	}
}

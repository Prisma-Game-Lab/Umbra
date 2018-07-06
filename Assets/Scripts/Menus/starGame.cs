using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class starGame : MonoBehaviour {

    public Text texto;

	// Use this for initialization
	void Start () {
        Time.timeScale = 0;
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        StarGame();
    }

    public void StarGame()
    {
        if (Input.anyKey)
        {
            Time.timeScale = 1f;
            Destroy(texto);
        }
    }
}

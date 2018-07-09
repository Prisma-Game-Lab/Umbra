using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    public string PauseMenuScene;
    public string MainMenuScene;

    
    // Update is called once per frame
    


    public void Resume()
    {

        Time.timeScale = 1f;
        SceneManager.UnloadSceneAsync(PauseMenuScene);
        
    }


    



    public void LoadMenu()
    {
        Debug.Log("Loading menu...");
        Time.timeScale = 1f;
        SceneManager.LoadScene(MainMenuScene);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}

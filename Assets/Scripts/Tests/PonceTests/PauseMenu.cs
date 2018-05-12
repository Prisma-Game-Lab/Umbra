using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPause = false;
    public string PauseMenuScene;

    public GameObject pauseMenuUI;
    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }


    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;
        if (!SceneManager.GetSceneByName(PauseMenuScene).isLoaded)
        {
            SceneManager.LoadScene(PauseMenuScene, LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.UnloadScene(PauseMenuScene);
        }
    }


    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
    }



    public void LoadMenu()
    {
        Debug.Log("Loading menu...");
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}

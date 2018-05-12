using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public string PauseMenuScene;
    public static bool GameIsPause = false;


    // Update is called once per frame
    public void Update()
    {
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
            if (!SceneManager.GetSceneByName(PauseMenuScene).isLoaded)
            {
                SceneManager.LoadScene(PauseMenuScene, LoadSceneMode.Additive);
            }
            else
            {
                SceneManager.UnloadScene(PauseMenuScene);
            }
          
           

        }

    }


    public void Resume()
    {

        Time.timeScale = 1f;
        GameIsPause = false;
        
    }


    void Pause()
    {
        Time.timeScale = 0f;
        GameIsPause = true;

    }

}

        
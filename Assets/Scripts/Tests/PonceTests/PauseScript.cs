using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{   
    
    public string PauseMenuScene;
    public bool GameIsPause = false;


    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) /// quando o jogador aperta escape, pausa o jogo, chama o menu de pause, e corrige o erro de gerar varios menus de pause
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
        if (SceneManager.GetSceneByName(PauseMenuScene).isLoaded) /// se o menu de pause estiver ativado "seta" a gameIsPause para verdade
        {
            GameIsPause = true;
        }
        else
        {
            GameIsPause = false;
        }
        

    }


    public void Resume() /// volta o tempo do jogo
    {
       
        Time.timeScale = 1f;
        

    }


    void Pause() /// para o tempo do jogo
    {
        Time.timeScale = 0f;
        
        

    }

}

        
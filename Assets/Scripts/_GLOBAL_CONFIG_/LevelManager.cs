using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{

    public int PlayerLayer;
    public string FirstLevelSceneName;
    public string[] SceneNames;
    private Animator animator;
    


	[Header("Sons")]	//som
	[Header("Sons da Hook")]
	[Tooltip("AudioSource que guarda o som de ida do hook")]public AudioSource HookGo;
	[Tooltip("Delay em segundos que o áudio começa a tocar")]public float HookGoDelay;
	[Tooltip("AudioSource que guarda o som de quando o hook acerta o cenário")]public AudioSource HookHitLevel;
	[Tooltip("Delay em segundos que o áudio começa a tocar")]public float HookHitLevelDelay;
	[Tooltip("AudioSource que guarda o som de quando o hook acerta o inimigo")]public AudioSource HookHitEnemy;
	[Tooltip("Delay em segundos que o áudio começa a tocar")]public float HookHitEnemyDelay;
	[Tooltip("AudioSource que guarda o som de volta do hook")]public AudioSource HookReturns;
	[Tooltip("Delay em segundos que o áudio começa a tocar")]public float HookReturnsDelay;

	[Header("Sons do Player")]
	[Tooltip("AudioSource que guarda o som do passo do player")]public AudioSource PlayerWalk;
	[Tooltip("Delay em segundos que o áudio começa a tocar")]public float PlayerWalkDelay;
	[Tooltip("AudioSource que guarda o som do pulo do player")]public AudioSource PlayerJump;
	[Tooltip("Delay em segundos que o áudio começa a tocar")]public float PlayerJumpDelay;
	[Tooltip("AudioSource que guarda o som do player morrendo")]public AudioSource PlayerDying;
	[Tooltip("Delay em segundos que o áudio começa a tocar")]public float PlayerDyingDelay;

	[Header("Sons do Inimigo")]
	[Tooltip("Som que toca quando o archer toma dano")] public AudioSource ArcherDamageSound;
	[Tooltip("Tempo em segundos que demora para o som começar a tocar")]public float ArcherDamageSoundDelay;
	[Tooltip("Som que toca quando o archer ataca")] public AudioSource ArcherAttackSound;
	[Tooltip("Tempo em segundos que demora para o som começar a tocar")]public float ArcherAttackSoundDelay;

	[Header("Sons do Cristal")]
	[Tooltip("Som que toca quando o cristal é absorvido")] public AudioSource CrystalAbsorbedSound;
	[Tooltip("Tempo em segundos que demora para o som começar a tocar")]public float CrystalAbsorbedSoundDelay;


	private int currentLevel = -1;

    void Start()
    {
        // Finds the scene that is currently being played and set current level to it.
        string currentSceneName = SceneManager.GetActiveScene().name;
        for (int i = 0; i < this.SceneNames.Length; i++)
        {
            if (currentSceneName == this.SceneNames[i])
            {
                // If found the scene, set the current level to it.
                this.currentLevel = i;
                break;
            }
        }
        

    }

    void Update()
    {
        if (animator == null)
        {
            // Finds the animator on canvas
            animator = GameObject.Find("Canvas").GetComponent<Animator>();
        }

        if (Input.GetKeyDown(KeyBindings.Instance.GameFlowReloadScene))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        
    }

    public void GoToLevel(int level)
    {
        if (level < SceneNames.Length && level >= 0)
        {
            currentLevel = level;
            SceneManager.LoadScene(SceneNames[level]);
        }
        else if (level == -1)
        {
            currentLevel = -1;
            SceneManager.LoadScene(FirstLevelSceneName);
            

        }
        else
        {
            Debug.LogError("LevelManager attempted to go to a level that does not exit. Level = " + level);
        }
    }

    public void GoToNextLevel()
    {
        FadeToLevel();
        
    }

    //Define o que ocorre ao final da animação de FadeOut. Chamada pela mesma.
    public void OnFadeOutEnd()
    {
        if (currentLevel + 1 < SceneNames.Length)
        {
            GoToLevel(currentLevel + 1);


        }
        else
        {
            GoToLevel(-1);

        }
    }

    public void ResetLevel()
    {
        GoToLevel(currentLevel);
    }

    public void FadeToLevel()
    {
        animator.SetTrigger("FadeOut");
    }


}

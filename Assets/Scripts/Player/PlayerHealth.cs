using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int HealthMax = 1;
    [Range(0,60)] public float TimeToDie = 1;

    [HideInInspector] public int CurrentHealth;

    private string ThisSceneName;

    IEnumerator DeathTimer(float timer)
    {
        this.gameObject.GetComponent<PlayerController>().enabled = false;
        yield return new WaitForSeconds(timer);
        PlayerDeath();
    }

    private void Start()
    {
        ThisSceneName = SceneManager.GetActiveScene().name;
        CurrentHealth = HealthMax;
    }

    private void FixedUpdate()
    {
        if(CurrentHealth <= 0)
        {
            
            //this.gameObject.GetComponent<PlayerController>().enabled = false;
            StartCoroutine(DeathTimer(TimeToDie));
        }
    }

    private void PlayerDeath()
    {
        SceneManager.LoadScene(ThisSceneName, LoadSceneMode.Single);
    }

}

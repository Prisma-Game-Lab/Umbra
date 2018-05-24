using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherHealth : MonoBehaviour
{
    public int HealthMax = 1;
    public ParticleSystem ParticleOfDeath;

    /*[HideInInspector]*/ public int CurrentHealth;
    private float _timerDeath = 0;

    IEnumerator Death()
    {
        //ParticleOfDeath.Play();
        yield return new WaitForSeconds(_timerDeath);
        Destroy(this);
    }

    private void Start()
    {
        CurrentHealth = HealthMax;
        //_timerDeath =  (ParticleOfDeath == null) ? 0 : ParticleOfDeath.time;
    }

    private void Update()
    {
        if(CurrentHealth <= 0)
        {
            StartCoroutine(Death());
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherHealth : MonoBehaviour
{
    public int HealthMax = 1;
    [Range(0, 60)] public float _timerDeath = 0;

    [HideInInspector] public int CurrentHealth;


    IEnumerator Death()
    {
        yield return new WaitForSeconds(_timerDeath);
        Destroy(this.gameObject);
    }

    private void Start()
    {
        CurrentHealth = HealthMax;
    }

    private void Update()
    {
        if(CurrentHealth <= 0)
        {
            StartCoroutine(Death());
        }
    }

}

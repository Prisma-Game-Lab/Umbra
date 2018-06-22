using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookDamage : MonoBehaviour
{
    public int Damage = 1;

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.CompareTag("Enemy"))
        {
			ArcherHealth enemy = col.GetComponent<ArcherHealth> ();
            enemy.CurrentHealth -= Damage;
			LevelManager.Instance.ArcherDamageSound.PlayDelayed(LevelManager.Instance.ArcherDamageSoundDelay);
        }
        
        /*
        if(col.GetComponent<GameObject>().layer == 12)
        {
            col.GetComponent<ArcherHealth>().CurrentHealth -= Damage;
        }*/
    }
}

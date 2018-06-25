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
            col.GetComponent<ArcherShot>().DisableShooting();
			LevelManager.Instance.ArcherDamageSound.PlayDelayed(LevelManager.Instance.ArcherDamageSoundDelay);
        }
        
    }
}

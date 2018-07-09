using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            //col.GetComponent<ArcherShot>()._hasHit = true;        // Useless
            col.GetComponent<NewArcherScript>().IsDead = true;
            col.GetComponent<NewArcherScript>().state = NewArcherScript.State.Dead;
			LevelManager.Instance.ArcherDamageSound.PlayDelayed(LevelManager.Instance.ArcherDamageSoundDelay);
        }
        
    }
}

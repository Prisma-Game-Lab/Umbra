using System.Collections;
using UnityEngine;

public class ArcherShot : MonoBehaviour
{
    public bool isArrowEnabled = true;
    public GameObject ArrowPrefab;
    [Range(0, 10)] public float ShotCooldown = 1;

    [HideInInspector] public bool _hasHit = false;

    private bool _canShoot = true;

    IEnumerator Cooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        _canShoot = true;
    }

    private void FixedUpdate()
    {
        if (!_hasHit)
        {
            if (_canShoot && isArrowEnabled)
            {
                // Instantiate as child
                // Instantiate<GameObject>(ArrowPrefab, this.transform.position, this.transform.rotation, this.transform);

                // Instantiate alone
                Instantiate<GameObject>(ArrowPrefab, this.transform.position, this.transform.rotation);
				LevelManager.Instance.ArcherAttackSound.PlayDelayed(LevelManager.Instance.ArcherAttackSoundDelay);

                _canShoot = false;
                StartCoroutine(Cooldown(ShotCooldown));
            }
        }
    }
}

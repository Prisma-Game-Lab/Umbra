using System.Collections;
using UnityEngine;

public class ArcherShot : MonoBehaviour
{
    public GameObject ArrowPrefab;
    [Range(1, 10)] public float ShotCooldown = 1;

    private bool _canShoot = true;

    IEnumerator Cooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        _canShoot = true;
    }

    private void FixedUpdate()
    {
        if (_canShoot)
        {
            Instantiate<GameObject>(ArrowPrefab, this.transform.position, this.transform.rotation, this.transform);
            _canShoot = false;
            StartCoroutine(Cooldown(ShotCooldown));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewArcherScript : MonoBehaviour
{
    [Header("Values")]
    [Tooltip("Cooldown after shooting")] [Range(0.1f, 5.0f)] public float Cooldown = 1;      // Cooldown after shooting
    [Tooltip("Timer after Lock-on to Player")] [Range(0.1f, 5.0f)] public float LockOnTimer = 1;   // Time after Lock-on to player
    [Tooltip("Max Look Rotation of the archer")] [Range(0f, 180f)] public float MaxLookRotation = 90;   // Time after Lock-on to player

    [Header("Things to drag here")]
    [Tooltip("Here is the arrow prefab. With this we can instantiate the arrow")] public GameObject ArrowPrefab;
    [Tooltip("Here is the BowRelease. From this point that the arrow will instantiate")] public GameObject BowRelease;
    [Tooltip("Here is the FOVCollider. This is the Field of View of the Archer")] public Collider2D FOVCollider;

    [HideInInspector]public bool _isDead = false;

    private bool _canShoot = false;
    private Transform _playerTransform;
    private Transform _middleLayerTransform;
    private Quaternion _ArrowRotation;


    #region State
    // Here you name the states
    public enum State
    {
        Idle,
        LockOn,
        Shooting,
        Cooldown,
        Dead,
    }
    public State state;

    IEnumerator IdleState()
    {
        while (state == State.Idle)
        {
            yield return 0;
        }
        NextState();
    }

    IEnumerator LockOnState()
    {
        while (state == State.LockOn)
        {
            yield return 0;
        }
        NextState();
    }

    IEnumerator ShootingState()
    {
        while (state == State.Shooting)
        {
            yield return 0;
        }
        NextState();
    }

    IEnumerator CooldownState()
    {
        while (state == State.Cooldown)
        {
            yield return 0;
        }
        NextState();
    }

    IEnumerator DeadState()
    {
        while (state == State.Dead)
        {
            yield return 0;
        }
        NextState();
    }

    void NextState()
    {
        string methodName = state.ToString() + "State";
        System.Reflection.MethodInfo info =
            GetType().GetMethod(methodName,
                                System.Reflection.BindingFlags.NonPublic |
                                System.Reflection.BindingFlags.Instance);
        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }
    #endregion
    /* 
     * STATES:
     * Idle
     * LockOn
     * Shooting
     * Cooldown
     * Dead
    */

    IEnumerator CooldownCoroutine()
    {
        _canShoot = false;
        yield return new WaitForSeconds(Cooldown);
        state = State.Idle;
    }

    IEnumerator LockOnCoroutine()
    {
        _canShoot = true;
        yield return new WaitForSeconds(LockOnTimer);
        state = State.Shooting;
    }

    private void Shoot()
    {
        Instantiate<GameObject>(ArrowPrefab, BowRelease.transform.position, BowRelease.transform.rotation, _middleLayerTransform);
        LevelManager.Instance.ArcherAttackSound.PlayDelayed(LevelManager.Instance.ArcherAttackSoundDelay);
    }

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _middleLayerTransform = _playerTransform.parent;
    }

    private void FixedUpdate()
    {
        if (!_isDead)
        {
            if (state == State.LockOn)
            {
                // This makes the enemy look at the player
                if (transform.rotation.z <= MaxLookRotation / 2 && transform.rotation.z >= -MaxLookRotation / 2)
                {
                    Vector3 diff = _playerTransform.position - transform.position;
                    diff.Normalize();
                    float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Clamp(rot_z, -MaxLookRotation / 2, MaxLookRotation / 2));
                }

                StartCoroutine(LockOnCoroutine());
            }
            if (state == State.Shooting)
            {
                if (_canShoot)
                {
                    Shoot();
                }
                state = State.Cooldown;
            }
            if (state == State.Cooldown)
            {
                StartCoroutine(CooldownCoroutine());
            }
        }
        else
        {
            if (state == State.Dead)
            {
                StopAllCoroutines();
            }
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!_isDead)
        {
            if (state == State.Idle)
            {
                if (other.CompareTag("Player"))
                {
                    state = State.LockOn;
                }
            }
        }
    }

}
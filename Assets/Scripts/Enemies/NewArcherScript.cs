using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewArcherScript : MonoBehaviour
{
    [Header("Values")]
    [Tooltip("Cooldown after shooting")] [Range(0.1f, 5.0f)] public float Cooldown = 1;
    [Tooltip("Timer after Lock-on to Player")] [Range(0.1f, 5.0f)] public float LockOnTimer = 1;
    [Tooltip("Max Look Rotation of the archer")] [Range(0f, 90f)] public float MaxLookRotation = 45;
    [Tooltip("Speed for the rotation to look at player")] [Range(0.1f, 100.0f)] public float RotationSpeed = 15;
    [Space]
    [Header("Things to drag here")]
    [Tooltip("Here is the arrow prefab. With this we can instantiate the arrow")] public GameObject ArrowPrefab;
    [Tooltip("Here is the BowRelease. From this point that the arrow will instantiate")] public GameObject BowRelease;
    [Tooltip("Here is the FOVCollider. This is the Field of View of the Archer")] public Collider2D FOVCollider;

    [HideInInspector] public bool IsDead = false;

    private bool _canShoot = false;
    private bool _playerInSight = false;
    private Transform _playerTransform;
    private Transform _middleLayerTransform;
    private Quaternion _ArrowRotation;
    private float _ArcherYRotation = 0;
    private Vector3 _startingAngle;
    private Vector3 _currentAngle;
    private Vector3 _targetAngle;

    #region StateMachine
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

    IEnumerator CooldownCoroutine()         // The coroutine to the cooldown after shooting the arror
    {
        _canShoot = false;
        yield return new WaitForSeconds(Cooldown);
        state = State.Idle;
    }

    IEnumerator LockOnCoroutine()           // The time it takes to lock on the target
    {
        _canShoot = true;
        yield return new WaitForSeconds(LockOnTimer);
        state = State.Shooting;
    }

    private void Shoot()        // Function to instantiate the arrow
    {
        Instantiate<GameObject>(
            ArrowPrefab,                            // The Prefab to instantiate
            BowRelease.transform.position,          // The Position to instantiate the Prefab
            BowRelease.transform.rotation,          // The Rotation to instantiate the Prefab
            _middleLayerTransform);                 // The position in the Hierarchy to instantiate the Prefab
        LevelManager.Instance.ArcherAttackSound.PlayDelayed(LevelManager.Instance.ArcherAttackSoundDelay);
    }

    private void LookAtLarp(Vector3 rotTarget)   // Function to make the Archer look at the Player
    {
        _currentAngle = transform.eulerAngles;

        _currentAngle = new Vector3(
            Mathf.LerpAngle(_currentAngle.x, rotTarget.x, Time.deltaTime),                       // Maintains the X axis without rotating
            Mathf.LerpAngle(_currentAngle.y, rotTarget.y, Time.deltaTime),                       // Maintains the Y axis without rotating
            Mathf.LerpAngle(_currentAngle.z, rotTarget.z, Time.deltaTime * RotationSpeed));      // Rotates only the Z axis

        this.transform.eulerAngles = _currentAngle;         // Here is where GameObject is rotated
    }

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _middleLayerTransform = _playerTransform.parent;
        _startingAngle = this.transform.eulerAngles;
    }

    private void FixedUpdate()
    {
        if (!IsDead)        // Check if dead
        {
            if (state == State.Idle)        // Check if correct state
            {
                //LookAtLarp(_startingAngle);
            }
            if (state == State.LockOn)      // Check if correct state
            {
                Vector3 diff = _playerTransform.position - transform.position;
                diff.Normalize();
                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

                if (this.transform.rotation.y == 0)
                {
                    if (rot_z >= 0 && rot_z <= 90)              // Positive-Right sector
                    {
                        _targetAngle = new Vector3(_currentAngle.x, _currentAngle.y, Mathf.Clamp(rot_z, 0, MaxLookRotation));
                        LookAtLarp(_targetAngle);
                    }
                    else if (rot_z >= -90 && rot_z < 0)         // Negative-Right sector
                    {
                        _targetAngle = new Vector3(_currentAngle.x, _currentAngle.y, Mathf.Clamp(rot_z, -MaxLookRotation, 0));
                        LookAtLarp(_targetAngle);
                    }
                }
                else
                {
                    if (rot_z > 90 && rot_z <= 180)        // Positive-Left sector
                    {
                        _targetAngle = new Vector3(_currentAngle.x, _currentAngle.y, Mathf.Clamp(180 - rot_z, 0, MaxLookRotation));
                        LookAtLarp(_targetAngle);
                    }
                    else if (rot_z >= -180 && rot_z < -90)      // Negative-Left sector
                    {
                        _targetAngle = new Vector3(_currentAngle.x, _currentAngle.y, Mathf.Clamp(-(180 - rot_z), -MaxLookRotation, 0));
                        LookAtLarp(_targetAngle);
                    }
                }

                //Debug.Log(rot_z);

                StartCoroutine(LockOnCoroutine());
            }
            if (state == State.Shooting)        // Check if correct state
            {
                if (_canShoot)
                    Shoot();
                state = State.Cooldown;       // Change the state
            }
            if (state == State.Cooldown)        // Check if correct state
            {
                StartCoroutine(CooldownCoroutine());
            }
        }
        else
        {
            if (state == State.Dead)
                StopAllCoroutines();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!IsDead)        // Check if dead
            if (state == State.Idle)        // Check if correct state
                if (other.CompareTag("Player"))     // Check if it's the Player that's inside the F.O.V.
                    state = State.LockOn;       // Change the state
    }
}
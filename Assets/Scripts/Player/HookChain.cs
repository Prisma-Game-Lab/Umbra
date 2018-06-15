using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookChain : MonoBehaviour
{
    public ParticleSystem ChainParticle;
    public GameObject Target;

    private Vector3 _targetPos;

    private void Start()
    {
        // _targetPos = Target.transform.position;
    }

    private void FixedUpdate()
    {
        _targetPos = Target.transform.position;

        // --- Put here the code to play the particle

        // Rotates the particle
        Vector3 diff = _targetPos - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        // --- Put here the code to stop the particle

    }


}

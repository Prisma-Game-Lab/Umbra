using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    [Header("Variables")]
    [Tooltip("Speed the camera will move to the final position")] [Range(0f, 100f)] public float PanSpeed = 10f;
    [Header("Final Position")]
    [Tooltip("Drag here the GameObject used to know the final position")] public Transform FinalPos;

    private bool _autoStartPan = true;
    private float _traveltime = 0;

    IEnumerator TravelTimeCoroutine()
    {
        yield return new WaitForSeconds(_traveltime);
        _autoStartPan = false;
    }

    private void Start()
    {
        // Calculates the travel time, based on the distance and the speed
        _traveltime = Vector3.Distance(this.transform.position, FinalPos.position) / PanSpeed;
    }

    private void FixedUpdate()
    {
        if (_autoStartPan)
        {
            // Start coroutine to know when to stop
            StartCoroutine(TravelTimeCoroutine());
            // Moves the camera at the desired speed
            this.transform.Translate(new Vector3(PanSpeed, 0f, 0f) * Time.deltaTime);
        }
    }
}

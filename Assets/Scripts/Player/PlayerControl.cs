using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerControl : MonoBehaviour
{
    public float Gravity = 10;
    public float Speed = 10;
    public float Acceleration = 10;
    public float JumpHeight = 10;

    private float _currentSpeed;
    private float _targetSpeed;
    private Vector3 _amountToMove;

    private PlayerPhysics _playerPhysics;

    private void Start()
    {
        _playerPhysics = GetComponent<PlayerPhysics>();
    }

    private void Update()
    {
        _targetSpeed = Input.GetAxisRaw("Horizontal") * Speed;
        _currentSpeed = IncrementTowards(_currentSpeed, _targetSpeed, Acceleration);
        
        if (_playerPhysics.IsGrounded)
        {
            _amountToMove.y = 0;

            if (Input.GetButtonDown("Jump"))  // Jump
            {
                _amountToMove.y = JumpHeight;
            }
        }

        _amountToMove.x = _currentSpeed;
        _amountToMove.y -= Gravity * Time.deltaTime;
        _playerPhysics.Move(_amountToMove * Time.deltaTime);
    }

    // Increase n towards target by a
    private float IncrementTowards(float n, float target, float a)  // n = current speed; a = acceleration
    {
        if (n == target) return n;
        else
        {
            float dir = Mathf.Sign(target - n);
            n += a * Time.deltaTime * dir;
            return (dir == Mathf.Sign(target - n)) ? n : target;
        }
    }

}
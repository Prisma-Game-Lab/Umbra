using UnityEngine;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour
{
    public float Gravity = 10;
    public float Speed = 10;
    public float Acceleration = 10;
    public float JumpHeight = 10;
    public int NumJumps = 2;

    private float _currentSpeed;
    private float _targetSpeed;
    private Vector3 _amountToMove;
    private int _currentAvailableJumps;
    private bool _wasGroundedLastUpdate;

    private PlayerPhysics _playerPhysics;

    private void Start()
    {
        _playerPhysics = GetComponent<PlayerPhysics>();
        this._currentAvailableJumps = this.NumJumps;
    }

    private void Update()
    {
        if (_playerPhysics.IsStopped)
        {
            _targetSpeed = 0;
            _currentSpeed = 0;
        }

        _targetSpeed = Input.GetAxisRaw("Horizontal") * Speed;
        _currentSpeed = IncrementTowards(_currentSpeed, _targetSpeed, Acceleration);


        if (_playerPhysics.IsGrounded)
        {
            _amountToMove.y = Mathf.Max(0.0f, _amountToMove.y);
        }
        else
        {
            _amountToMove.y -= Gravity * Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))  // Jump
        {
            if(this._currentAvailableJumps > 0)
            {
                this._currentAvailableJumps -= 1;
                _amountToMove.y = JumpHeight;
            }
        }

        if (_playerPhysics.IsGrounded == true && this._wasGroundedLastUpdate == false)
        {
            this._currentAvailableJumps = this.NumJumps;
        }

        this._wasGroundedLastUpdate = _playerPhysics.IsGrounded;
        _amountToMove.x = _currentSpeed;
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
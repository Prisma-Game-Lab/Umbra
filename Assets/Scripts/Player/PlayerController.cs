using UnityEngine;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour
{
    #region Public
    public PauseScript PauseScript;

    public float Gravity = 10;
    public float Speed = 10;
    public float Acceleration = 10;
    public float JumpHeight = 10;
    public int NumJumps = 2;

    public float HookLaunchSpeed = 100.0f;
    public float HookPullSpeed = 100.0f;
    public float HookPullFinishDistance = 2.0f;

    public float HookMaxDistance = 20.0f;
    public LayerMask HookCollisionLayerMask;
    public LayerMask BreakCollisionLayerMask;
    public LayerMask EnemyCollisionLayerMask;

    public GameObject HookPrefab;
    #endregion

    #region Private
    private float _currentSpeed;
    private float _targetSpeed;
    private Vector3 _amountToMove;
    private int _currentAvailableJumps;
    private bool _wasGroundedLastUpdate;
    private bool _isHookOnLaunchPhase = false;
    private bool _isHookOnPullPhase = false;
    private GameObject _hookInstantiated;
    private CircleCollider2D _hookCollider;
    private SpriteRenderer mySpriteRenderer;

	//som
	public AudioSource hookGo;

    private PlayerPhysics _playerPhysics;
    #endregion

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, this.HookPullFinishDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, this.HookMaxDistance);
    }

    private void Start()
    {
        _playerPhysics = GetComponent<PlayerPhysics>();
        this._currentAvailableJumps = this.NumJumps;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void updateCurrentSpeed()
    {
        if (_playerPhysics.IsBlockedHorizontally)
        {
            _targetSpeed = 0;
            _currentSpeed = 0;
            
        }

        _targetSpeed = Input.GetAxisRaw("Horizontal") * Speed;
        _currentSpeed = IncrementTowards(_currentSpeed, _targetSpeed, Acceleration);
        _amountToMove.x = _currentSpeed;
        
    }

    private void updateWithGravity()
    {
        if (_playerPhysics.IsBlockedVertically)
        {
            _amountToMove.y = 0.0f;
        }

        if (_playerPhysics.IsGrounded)
        {
            _amountToMove.y = Mathf.Max(0.0f, _amountToMove.y);
            
        }
        else
        {
            _amountToMove.y -= Gravity * Time.deltaTime;
           
        }
    }

    private void updateWithJump()
    {
        if (Input.GetButtonDown("Jump"))  // Jump
        {
            if (this._currentAvailableJumps > 0)
            {
                this._currentAvailableJumps -= 1;
                _amountToMove.y = JumpHeight;
                this.resetHook();
            }

        }

        if (_playerPhysics.IsGrounded == true && this._wasGroundedLastUpdate == false)
        {
            this._currentAvailableJumps = this.NumJumps;
        }

        this._wasGroundedLastUpdate = _playerPhysics.IsGrounded;
    }

    private void resetHook()
    {
        this._hookCollider = null;
        if (this._hookInstantiated != null)
        {
            Destroy(this._hookInstantiated);
        }
        this._hookInstantiated = null;
        this._isHookOnLaunchPhase = false;
        this._isHookOnPullPhase = false;
    }

    private void updateWithHook()
    {
        if (Input.GetKeyDown(KeyBindings.Instance.playerHook) && PauseScript.GameIsPause == false)
        {
            if (!_isHookOnPullPhase && !_isHookOnLaunchPhase)
            {
                this._isHookOnLaunchPhase = true;
                Vector3 mouseClick3dRelativeToPlayer = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
                Vector2 mouseClick2dRelativeToPlayer = new Vector2(mouseClick3dRelativeToPlayer.x, mouseClick3dRelativeToPlayer.y);
                Vector2 xAxis = new Vector2(1.0f, 0.0f);
                float rotationAngle = Vector2.SignedAngle(xAxis, mouseClick2dRelativeToPlayer);
                Quaternion hookRotation = new Quaternion();
                hookRotation.eulerAngles = new Vector3(0.0f, 0.0f, rotationAngle);

                this._hookInstantiated = Instantiate(this.HookPrefab, this.transform.position, hookRotation);
                this._hookCollider = this._hookInstantiated.GetComponent<CircleCollider2D>();

				hookGo = GameObject.Find("Hook Go").GetComponent<AudioSource>();
				hookGo.Play ();
            }
        }
        if (_isHookOnLaunchPhase)
        {
            Vector3 localTranslation = new Vector3(1.0f, 0.0f, 0.0f) * this.HookLaunchSpeed * Time.deltaTime;
            this._hookInstantiated.transform.localPosition += this._hookInstantiated.transform.TransformVector(localTranslation);
            if ((this._hookInstantiated.transform.position - this.transform.position).magnitude > HookMaxDistance) {
                this.resetHook();
            }
            else if (this._hookCollider.IsTouchingLayers(this.BreakCollisionLayerMask))
            {
                this.resetHook();
            }
            else if (this._hookCollider.IsTouchingLayers(this.HookCollisionLayerMask))
            {
                this._isHookOnLaunchPhase = false;
                this._isHookOnPullPhase = true;
                this._currentAvailableJumps = this.NumJumps;
            }
            else if (this._hookCollider.IsTouchingLayers(this.EnemyCollisionLayerMask))
            {
                this._isHookOnLaunchPhase = false;
                this._isHookOnPullPhase = true;
                this._currentAvailableJumps = this.NumJumps;
            }
        }
        if (_isHookOnPullPhase)
        {
            Vector2 deltaPosition = this._hookInstantiated.transform.position - this.transform.position;
            this._amountToMove = new Vector2(0.0f, 0.0f);
            if (deltaPosition.magnitude <= this.HookPullFinishDistance)
            {
                this.resetHook();
            }
            else
            {
                this._amountToMove = deltaPosition.normalized * this.HookPullSpeed;
            }
        }
    }

    private void Update()
    {
        this.updateCurrentSpeed();
        this.updateWithGravity();
        this.updateWithJump();
        this.updateWithHook();

        if(_amountToMove.x > 0)
        {
            mySpriteRenderer.flipX = true;
        }
        else
        {
            mySpriteRenderer.flipX = false;
        }

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
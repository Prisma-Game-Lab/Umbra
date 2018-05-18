using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerPhysics : MonoBehaviour
{
    // This sets on what the player id as ground. The ground must have the selected collider.
    public LayerMask CollisionMask;

    [HideInInspector] public bool IsGrounded;  // When touching the ground
    [HideInInspector] public bool IsBlockedVertically;  // When touching collision on the sides
    [HideInInspector] public bool IsBlockedHorizontally;  // When touching collision on the sides

    private BoxCollider2D _colllider;
    private Vector3 _colSize;
    private Vector3 _colCenter;
    private float skin = 0.005f;  // Space between the player and the ground, otherwise it can't detect the ground
    private Ray2D _ray;
    private RaycastHit2D _hit;

    private void Start()
    {
        _colllider = GetComponent<BoxCollider2D>();
        _colSize = _colllider.size;
        _colCenter = _colllider.offset;
    }

    public void Move(Vector2 moveAmount)
    {
        float deltaX = moveAmount.x;
        float deltaY = moveAmount.y;
        Vector2 pos = transform.position;

        // Check if is Grounded after movement
        IsGrounded = false;
        if (deltaY <= float.Epsilon) {
            for (int i = 0; i < 3; i++)
            {
                float dir = -1;
                float x = (pos.x + _colCenter.x - _colSize.x / 2) + _colSize.x / 2 * i;
                float y = pos.y + _colCenter.y + _colSize.y / 2 * dir;

                _ray = new Ray2D(new Vector2(x, y), new Vector2(0, dir));
                Debug.DrawRay(_ray.origin, _ray.direction, Color.green);
                
                _hit = Physics2D.Raycast(new Vector2(x, y), new Vector2(0, dir), Mathf.Abs(deltaY) + skin, CollisionMask);
                if (_hit.collider != null)
                {
                    IsGrounded = true;
                    break;
                }
            }
        }

        // Check collision up/down
        IsBlockedVertically = false;
        for (int i = 0; i < 3; i++)
        {
            float dir = Mathf.Sign(deltaY);
            float x = (pos.x + _colCenter.x - _colSize.x / 2) + _colSize.x / 2 * i;
            float y = pos.y + _colCenter.y + _colSize.y / 2 * dir;

            _ray = new Ray2D(new Vector2(x, y), new Vector2(0, dir));
            Debug.DrawRay(_ray.origin, _ray.direction, Color.red);
            
            _hit = Physics2D.Raycast(new Vector2(x, y), new Vector2(0, dir), Mathf.Abs(deltaY) + skin, CollisionMask);
            if (_hit.collider != null)
            {
                float dist = Vector2.Distance(_ray.origin, _hit.point);

                if (dist > skin)
                    deltaY = (dist - skin) * dir;
                else
                    deltaY = 0.0f;
                IsBlockedVertically = true;
                break;
            }
        }

        // Check collision left/right
        IsBlockedHorizontally = false;
        for (int i = 0; i < 3; i++)
        {
            float dir = Mathf.Sign(deltaX);
            float x = pos.x + _colCenter.x + _colSize.x / 2 * dir;
            float y = (pos.y + _colCenter.y - _colSize.y / 2) + _colSize.y / 2 * i;

            _ray = new Ray2D(new Vector2(x, y), new Vector2(dir, 0));
            Debug.DrawRay(_ray.origin, _ray.direction, Color.blue);

            _hit = Physics2D.Raycast(new Vector2(x, y), new Vector2(dir, 0), Mathf.Abs(deltaX) + skin, CollisionMask);
            if (_hit.collider != null)
            {
                float dist = Vector2.Distance(_ray.origin, _hit.point);

                if (dist > skin)
                    deltaX = (dist - skin) * dir;
                else
                    deltaX = 0.0f;
                IsBlockedHorizontally = true;
                break;
            }
        }

        deltaX = Mathf.Abs(deltaX) < float.Epsilon ? 0.0f : deltaX;
        deltaY = Mathf.Abs(deltaY) < float.Epsilon ? 0.0f : deltaY;
        Vector2 finalTransform = new Vector2(deltaX, deltaY);

        transform.Translate(finalTransform);
    }
}

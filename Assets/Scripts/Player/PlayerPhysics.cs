using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlayerPhysics : MonoBehaviour
{
    // This sets on what the player id as ground. The ground must have the selected collider.
    public LayerMask CollisionMask;

    [HideInInspector] public bool IsGrounded;  // When touching the ground
    [HideInInspector] public bool CanDoubleJump;  // Sets if the player can double jump
    [HideInInspector] public bool IsStopped;  // When touching collision on the sides

    private BoxCollider _colllider;
    private Vector3 _colSize;
    private Vector3 _colCenter;
    private float skin = 0.005f;  // Space between the player and the ground, otherwise it can't detect the ground
    private Ray _ray;
    private RaycastHit _hit;

    private void Start()
    {
        _colllider = GetComponent<BoxCollider>();
        _colSize = _colllider.size;
        _colCenter = _colllider.center;
    }

    public void Move(Vector2 moveAmount)
    {
        float deltaX = moveAmount.x;
        float deltaY = moveAmount.y;
        Vector2 pos = transform.position;

        // Check collision up/down
        IsGrounded = false;
        for (int i = 0; i < 3; i++)
        {
            float dir = Mathf.Sign(deltaY);
            float x = (pos.x + _colCenter.x - _colSize.x / 2) + _colSize.x / 2 * i;
            float y = pos.y + _colCenter.y + _colSize.y / 2 * dir;

            _ray = new Ray(new Vector2(x, y), new Vector2(0, dir));
            Debug.DrawRay(_ray.origin, _ray.direction, Color.red);

            if (Physics.Raycast(_ray, out _hit, Mathf.Abs(deltaY) + skin, CollisionMask))
            {
                float dist = Vector3.Distance(_ray.origin, _hit.point);

                if (dist > skin)
                    deltaY = (-dist + skin) * dir;
                else
                    deltaY = 0;
                IsGrounded = true;
                CanDoubleJump = true;
                break;
            }
        }

        // Check collision left/right
        IsStopped = false;
        for (int i = 0; i < 3; i++)
        {
            float dir = Mathf.Sign(deltaX);
            float x = pos.x + _colCenter.x + _colSize.x / 2 * dir;
            float y = (pos.y + _colCenter.y - _colSize.y / 2) + _colSize.y / 2 * i;
            

            _ray = new Ray(new Vector2(x, y), new Vector2(dir, 0));
            Debug.DrawRay(_ray.origin, _ray.direction, Color.blue);

            if (Physics.Raycast(_ray, out _hit, Mathf.Abs(deltaX) + skin, CollisionMask))
            {
                float dist = Vector3.Distance(_ray.origin, _hit.point);

                if (dist > skin)
                    deltaX = (-dist + skin) * dir;
                else
                    deltaX = 0;
                IsStopped = true;
                break;
            }
        }

        Vector2 finalTransform = new Vector3(deltaX, deltaY);

        transform.Translate(finalTransform);
    }
}

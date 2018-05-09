using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlayerPhysics : MonoBehaviour
{
    public LayerMask CollisionMask;

    [HideInInspector] public bool IsGrounded;

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

        IsGrounded = false;
        for (int i = 0; i < 3; i++)
        {
            float dir = Mathf.Sign(deltaY);
            float x = (pos.x + _colCenter.x - _colSize.x / 2) + _colSize.x / 2 * i;
            float y = pos.y + _colCenter.y + _colSize.y / 2 * dir;

            _ray = new Ray(new Vector2(x, y), new Vector2(0, dir));
            Debug.DrawRay(_ray.origin, _ray.direction, Color.red);

            if (Physics.Raycast(_ray, out _hit, Mathf.Abs(deltaY), CollisionMask))
            {
                float dist = Vector3.Distance(_ray.origin, _hit.point);

                if (dist > skin)
                    deltaY = -dist + skin;
                else
                    deltaY = 0;
                IsGrounded = true;
                break;
            }
        }

        Vector2 finalTransform = new Vector3(deltaX, deltaY);
        
        transform.Translate(finalTransform);
    }
}

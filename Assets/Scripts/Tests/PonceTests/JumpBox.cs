using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBox : MonoBehaviour {

    [Range(1, 10)]
    public float jumpVelocity;
    public float groundedSkin = 0.05f;
    public LayerMask mask;

    bool jumpRequest;
    bool grounded;

    Vector2 playerSize;
    Vector2 boxSize;


    void Awake()
    {
        playerSize = GetComponent<BoxCollider2D>().size;
        boxSize = new Vector2(playerSize.x, groundedSkin);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyBindings.Instance.PlayerJump) && grounded)
        {
            jumpRequest = true;
        }
    }

    void FixedUpdate()
    {
        if (jumpRequest)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);

            jumpRequest = false;
            grounded = false;
        }
        else
        {
            Vector2 boxCenter = (Vector2)transform.position + Vector2.down * (playerSize.y + boxSize.y) * 0.5f;
            grounded = (Physics2D.OverlapBox(boxCenter,boxSize,0f,mask) != null);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour {
    public float fallMultiplier = 2.5f; // how much we will multiply the gravity when the player is falling down
    public float lowJumpMultiplier = 2f; // multiply the gravity

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate() // here you can control gravity from unity
    {
        if (rb.velocity.y < 0) // when the player is falling
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0 && !Input.GetKeyDown(KeyBindings.Instance.PlayerJump))
        {

            rb.gravityScale = lowJumpMultiplier;
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }
}

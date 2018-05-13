using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpRayCast : MonoBehaviour {
        [Range(1, 10)]
        public float jumpVelocity;
        public float groundedSkin = 0f;
        public LayerMask mask;

        bool jumpRequest;
        bool grounded;

        Vector2 playerSize;


        void Awake()
        {
            playerSize = GetComponent<BoxCollider2D>().size;   
        }

        void Update()
        {
            if (Input.GetButtonDown("Jump") && grounded)
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
                Vector2 rayStart = (Vector2)transform.position + Vector2.down * playerSize.y * 0.5f;
                grounded = Physics2D.Raycast(rayStart, Vector2.down, groundedSkin, mask);
            }
        }


}

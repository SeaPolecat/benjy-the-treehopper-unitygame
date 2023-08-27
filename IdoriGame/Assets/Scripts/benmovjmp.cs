using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * ATTACHED TO:
 * -Player
 */

public class benmovjmp : MonoBehaviour
{
    public float jumpForce; // how strong Benjy's jump is
    public Transform groundCheck; // the position of the ground check circle at Benjy's feet
    public float groundCheckRadius; // the radius of the ground check circle
    public LayerMask groundLayer; // the layer that defines what the ground (branches) is
    public float jumpTime; // how long the player can hold jump for

    private Rigidbody2D rb; // the rigid body component of the player
    private bool isTouchingGround; // whether or not the player is touching the ground
    private float jumpTimeCounter; // a timer that decreases as the jump key is held down; used for the hold jump mechanic
    private bool isJumping; // whether or not the player is in a jumping state

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        /**
         * Physics2D.OverlapCircle:
         * gets the position of groundCheck (an empty object at the player's feet),
         * draws a circle of a specified radius around this position, and
         * checks if that circle is touching the groundLayer
         */
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // make the player jump when the jump key is pressed
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
            && isTouchingGround)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;

            rb.velocity = Vector2.up * jumpForce;
        }

        // continuously propel the player forward, if the jump key is held down
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
            && isJumping)
        {
            if(jumpTimeCounter > 0)
            {
                // continuously decrease jumpTimeCounter, if it's not 0 yet
                jumpTimeCounter -= Time.deltaTime;

                rb.velocity = Vector2.up * jumpForce;
            }
            else
            {
                // if jumpTimeCounter reaches 0, stop jumping
                isJumping = false;
            }
        }

        // stop jumping when the player releases the jump key
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            isJumping = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // delete Benjy if he touches the killzone (bottom border)
        if (other.tag == "KillZone")
        {
            Destroy(gameObject);
        }
    }
}

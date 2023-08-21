using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class benmovjmp : MonoBehaviour
{
    /*private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    // we use SerializeField to prevent other code from modifying this (this only becomes modifiable in the inspector)
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;*/

    public float jumpForce;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isTouchingGround;

    private void Start()
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

        if(Input.GetKeyDown(KeyCode.Space) && isTouchingGround)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        /*horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();*/
    }

    /*private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }*/
}
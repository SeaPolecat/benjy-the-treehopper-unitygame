using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class benmovjmp : MonoBehaviour
{
    public LayerMask groundLayer; // the layer that defines what the ground (branches) is
    public float groundCheckRadius; // the radius of the ground check circle
    public float jumpTime; // how long the player can hold jump for (in secs)
    public float bananaTime;
    public float jumpForce;
    public float bananaJumpForce;
    public GameObject jumpParticles;
    public GameObject controlsImage;
    //
    public Transform T_GroundCheck; // the position of the ground check circle at Benjy's feet
    public Animator AN_BananaUIImage;
    public ParticleSystem PS_JumpParticles;
    public AudioSource AS_JumpSound;
    public AudioSource AS_BoingSound;

    private bool isTouchingGround; // whether or not the player is touching the ground
    private bool isJumping; // whether or not the player is in a jumping state
    private float jumpTimeCounter; // a timer that decreases as the jump key is held down; used for the hold jump mechanic
    private float bananaTimeCounter;
    //
    private Rigidbody2D RB_Player; // the rigid body component of the player
    private Animator AN_PlayerSprite;

    void Start()
    {
        RB_Player = GetComponent<Rigidbody2D>();
        AN_PlayerSprite = GetComponentInChildren<Animator>();

        AN_PlayerSprite.speed = 0;
    }

    void Update()
    {
        /**
         * Physics2D.OverlapCircle:
         * gets the position of groundCheck (an empty object at the player's feet),
         * draws a circle of a specified radius around this position, and
         * checks if that circle is touching the groundLayer
         */
        isTouchingGround = Physics2D.OverlapCircle(T_GroundCheck.position, groundCheckRadius, groundLayer);

        // make the player jump when the jump key is pressed
        if ((Input.GetKeyDown(KeyCode.Space) && isTouchingGround))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;

            RB_Player.velocity = Vector2.up * jumpForce;

            if (jumpForce == bananaJumpForce)
            {
                jumpParticles.transform.position = transform.position + new Vector3(0, -0.5f, 0);

                PS_JumpParticles.Play();
                AS_BoingSound.Play();
            }
            else
            {
                AS_JumpSound.Play();
            }

            if (!Menu.gameStarted)
            {
                Menu.gameStarted = true;
                Destroy(controlsImage.gameObject);

                AN_PlayerSprite.speed = 1;
            }
        }

        // continuously propel the player forward, if the jump key is held down
        if ((Input.GetKey(KeyCode.Space) && isJumping))
        {
            if(jumpTimeCounter > 0)
            {
                // continuously decrease jumpTimeCounter, if it's not 0 yet
                jumpTimeCounter -= Time.deltaTime;

                RB_Player.velocity = Vector2.up * jumpForce;
            }
            else
            {
                // if jumpTimeCounter reaches 0, stop jumping
                isJumping = false;
            }
        }

        // stop jumping when the player releases the jump key
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        if (bananaTimeCounter > 0)
        {
            bananaTimeCounter -= Time.deltaTime;
        }
        else if (jumpForce != 10)
        {
            jumpForce = 10;

            AN_BananaUIImage.Play("BananaFadeOut");
        }
    }

    public void EatBanana()
    {
        bananaTimeCounter = bananaTime;
        jumpForce = bananaJumpForce;

        AN_BananaUIImage.enabled = true;
        AN_BananaUIImage.Play("BananaFadeIn");
    }
}

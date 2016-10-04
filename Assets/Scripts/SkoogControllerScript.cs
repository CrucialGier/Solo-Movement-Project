using UnityEngine;
using System.Collections;
using System;

public class SkoogControllerScript : MonoBehaviour
{

    public float speed = 10f;
    public float maxSpeed;
    public float idleDelayRange;
    public int differentIdles;
    public float jumpForce;
    public Transform groundCheck;
    public LayerMask whatIsGround;

    private Rigidbody2D rb2d;
    private bool isIdle;
    private float idleCount;
    private bool facingRight = false;
    private bool isGrounded = false;
    private float groundRadius = 0.1f;


    Animator skoogAnimator;

    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        skoogAnimator = GetComponent<Animator>();
        idleCount = UnityEngine.Random.Range(3, idleDelayRange);
    }
	
	void FixedUpdate()
    {
        Movement();
        Idle();
    }

    void Update()
    {
        Jump();
    }

    void Movement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        skoogAnimator.SetFloat("groundSpeed", Mathf.Abs(rb2d.velocity.x));
        Vector2 movement = new Vector2(moveHorizontal, 0);

        if (Mathf.Abs(rb2d.velocity.x) < maxSpeed)
        {
            rb2d.AddForce(movement * speed, 0);
        }

        if (moveHorizontal > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveHorizontal < 0 && facingRight)
        {
            Flip();
        }
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        skoogAnimator.SetBool("OnGround", isGrounded);
        skoogAnimator.SetFloat("vSpeed", rb2d.velocity.y);
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            skoogAnimator.SetBool("OnGround", false);
            rb2d.AddForce(new Vector2(0, jumpForce));
        }
    }

    void Flip()
    { 
        facingRight = !facingRight;
        Vector3 direction = transform.localScale;
        direction.x *= -1;
        transform.localScale = direction;
    }

    void Idle()
    {
        if (rb2d.velocity == new Vector2(0, 0))
        {
            idleCount -= Time.deltaTime;
            if (idleCount <= 0)
            {
                skoogAnimator.SetInteger("Idle", 1);
                idleCount = UnityEngine.Random.Range(12, idleDelayRange);
            }
            else
            {
                skoogAnimator.SetInteger("Idle", 0);
            }
        }
    }
}

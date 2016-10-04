using UnityEngine;
using System.Collections;
using System;

public class SkoogControllerScript : MonoBehaviour
{

    public float speed = 10f;
    public float maxSpeed;
    public float idleDelayRange;
    public int differentIdles;

    private bool isIdle;
    private float idleCount;
    private bool facingRight = false;
    private Rigidbody2D rb2d;

    Animator skoogAnimator;

    void Start ()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        skoogAnimator = GetComponent<Animator>();
        idleCount = UnityEngine.Random.Range(3, idleDelayRange);
    }
	
	void FixedUpdate ()
    {
        Movement();
        IdleCheck();
    }

    void Movement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        skoogAnimator.SetFloat("Speed", Mathf.Abs(rb2d.velocity.magnitude));
        Vector2 movement = new Vector2(moveHorizontal, rb2d.velocity.y);

        if (rb2d.velocity.magnitude < maxSpeed)
        {
            rb2d.AddForce(movement * speed);
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

    void Flip()
    { 
        facingRight = !facingRight;
        Vector3 direction = transform.localScale;
        direction.x *= -1;
        transform.localScale = direction;
    }

    void IdleCheck()
    {
        if (rb2d.velocity.magnitude == 0)
        {
            isIdle = true;
            IdleTimer();
        }
        else
        {
            isIdle = false;
        }
    }

    void IdleTimer()
    {
        idleCount -= Time.deltaTime;
        if (idleCount <= 0)
        {
            skoogAnimator.SetInteger("Idle", 1);
            Debug.Log(skoogAnimator.GetInteger("Idle"));
            idleCount = UnityEngine.Random.Range(12, idleDelayRange);
        }
        else
        {
            skoogAnimator.SetInteger("Idle", 0);
        }
    }
}

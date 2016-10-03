using UnityEngine;
using System.Collections;
using System;

public class SkoogControllerScript : MonoBehaviour
{

    public float speed = 10f;
    public float maxSpeed;

    private bool facingRight = false;
    private Rigidbody2D rb2d;

    Animator skoogAnimator;

    void Start ()
    {
       rb2d = gameObject.GetComponent<Rigidbody2D>();
        skoogAnimator = GetComponent<Animator>();
	}
	
	void FixedUpdate ()
    {
        Movement();
	}

    void Movement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        skoogAnimator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
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
}

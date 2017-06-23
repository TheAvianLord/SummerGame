using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    public float speed;
    public bool facingRight;
    public Transform[] groundPoints;
    public float groundRadius;
    public LayerMask whatIsGround;
    public bool isGrounded;
    public bool jump;
    public float jumpForce;

	// Use this for initialization
	void Start ()
    {
        facingRight = true;
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float horizontal = Input.GetAxis("Horizontal");
        isGrounded = IsGrounded();
        HandleMovement(horizontal);
        Flip(horizontal);
	}

    private void HandleMovement(float horizontal)
    {
        myRigidbody.velocity = new Vector2(horizontal * speed, myRigidbody.velocity.y);
        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));

        if (isGrounded && jump)
        {
            isGrounded = false;
            myRigidbody.AddForce(new Vector2(0, jumpForce));
        }

    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;

            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
    }

    private bool IsGrounded()
    {
        if (myRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundPosition, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }

        }
        return false;
    }
}

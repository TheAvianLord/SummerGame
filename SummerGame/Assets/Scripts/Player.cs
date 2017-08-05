using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody myRigidbody;
    private Animator myAnimator;
    public float speed;
    public bool facingRight;
    public Transform[] groundPoints;
    public float groundRadius;
    public LayerMask whatIsGround;
    public bool isGrounded;
    public bool jump;
    public bool z_jump_up;
    public bool z_jump_down;

	// Use this for initialization
	void Start ()
    {
        Physics.gravity = new Vector3(0, -20F, 0);
        facingRight = true;
        myRigidbody = GetComponent<Rigidbody>();
        myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float horizontal = Input.GetAxis("Horizontal");
        isGrounded = IsGrounded();
        HandleInput();
        HandleMovement(horizontal);
        Flip(horizontal);
        HandleLayers();
        
        ResetValues();
	}

    private void HandleMovement(float horizontal)
    {
        myRigidbody.velocity = new Vector2(horizontal * speed, myRigidbody.velocity.y);
        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));

        if (myRigidbody.velocity.y < 0)
        {
            myAnimator.SetBool("land", true);
        }

        if (isGrounded && jump)
        {
            isGrounded = false;
            myRigidbody.AddForce(new Vector3(0, 200, 0), ForceMode.Impulse);
            myAnimator.SetTrigger("jump");
        }

        if (isGrounded && z_jump_up && myRigidbody.position.z < 2.5)
        {
            isGrounded = false;
            myRigidbody.AddForce(new Vector3(0, 200, 5000), ForceMode.Impulse);
        }

        if (isGrounded && z_jump_down && myRigidbody.position.z > 2.5)
        {
            isGrounded = false;
            myRigidbody.AddForce(new Vector3(0, 200, -5000), ForceMode.Impulse);
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

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            z_jump_up = true;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            z_jump_down = true;
        }
    }

    private bool IsGrounded()
    {
        if (myRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider[] colliders = Physics.OverlapSphere(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        myAnimator.ResetTrigger("jump");
                        myAnimator.SetBool("land", false);
                        return true;
                    }
                }
            }

        }
        return false;
    }

    private void ResetValues()
    {
        jump = false;
        z_jump_up = false;
        z_jump_down = false;
    }

    private void HandleLayers()
    {
        if (!isGrounded)
        {
            myAnimator.SetLayerWeight(1, 1);
        }

        else
        {
            myAnimator.SetLayerWeight(1, 0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float xInput;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    [Header("Collision Detection")]
    [SerializeField] private float circleRadius;
    [SerializeField] private Transform circleMid;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float dashForce;

    private bool isFacingRight;
    private bool isDashing;
    private bool isWalking;
    private bool isGrounded;
    private float jumpCount;
    private Animator myAnimator;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        isFacingRight = true;
    }

    // Update is called once per frame

    void Update()
    {
        AnimatorController();
        FaceDirection();
        isGrounded = Physics2D.OverlapCircle(circleMid.position, circleRadius, groundLayer);
        isWalking = rb.velocity.x != 0;

        Jump();
        Dash();
    }

    void FixedUpdate()
    {
        xInput = Input.GetAxis("Horizontal");

        //Left to right = Horizontal Axis
        //Up to Down = Vertical Axis
        Move();
    }

    void FaceDirection()
    {
        if (xInput < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            isFacingRight = false;
        }
        else if (xInput > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            isFacingRight = true;
        }
    }

    void AnimatorController()
    {
        myAnimator.SetBool("isWalking", isWalking);
        myAnimator.SetBool("isJump", !isGrounded);
        myAnimator.SetFloat("yVelocity", rb.velocity.y);
    }

    void Move()
    {
        if (!isDashing)
        {
            rb.velocity = new Vector2(xInput * speed, rb.velocity.y);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount = 1; 
        } else if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount = 0;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(circleMid.position, circleRadius);
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            StartCoroutine(DashCoroutine());
        }
    }

    IEnumerator DashCoroutine()
    {
        isDashing = true;
        rb.velocity = Vector2.zero;
        if (isFacingRight)
        {
            rb.velocity = new Vector2(dashForce, rb.velocity.y);
        }
        else if
        (!isFacingRight)
        {
            rb.velocity = new Vector2(-dashForce, rb.velocity.y);
        }

        yield return new WaitForSeconds(.5f);
        isDashing = false; 
        rb.velocity = Vector2.zero; 
    }
}

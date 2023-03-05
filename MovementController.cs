using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Animator animator;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    [SerializeField]
    private float moveSpeed = 8f;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private float acceleration = 12f;
    [SerializeField]
    private float deccelaration = 12f;
    [SerializeField]
    private float accelRate = 2f;
    [SerializeField]
    private float velPower = .9f;
    [SerializeField]
    private float jumpForce = 6f;

    private bool facingRight = true;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if(isGrounded() && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
        {
            animator.SetBool("Jump", true);
            rb.AddForce(Vector2.up * jumpForce , ForceMode2D.Impulse);
        }
    }


    void FixedUpdate()
    {
        animator.SetBool("Jump", !isGrounded());
        animator.SetBool("Land", isGrounded());
        float moveInput = Input.GetAxis("Horizontal");

        float target = moveInput * moveSpeed;
        float diff = target - rb.velocity.x;
        float accel = (Mathf.Abs(target) > .01f) ? acceleration : deccelaration;
        float movement = Mathf.Pow(Mathf.Abs(diff) * accelRate, velPower) * Mathf.Sign(diff);

        animator.SetFloat("Run", Mathf.Abs(movement));

        rb.AddForce(movement * Vector2.right);

        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }

        if (moveInput < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
    }

    private bool isGrounded()
    {
        RaycastHit2D raycast = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, groundLayer);
        return raycast.collider != null;
    }

}

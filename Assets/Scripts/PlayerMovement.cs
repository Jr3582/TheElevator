using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 18f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        if (rb.velocity.y < 0 && !isGrounded()) {
            animator.SetBool("IsFalling", true);

        } else if (rb.velocity.y > 0) {
            animator.SetBool("IsFalling", false);

        } else if (isGrounded()) {
            animator.SetBool("IsGrounded", true);

            animator.SetBool("IsFalling", false);

        }

        if(Input.GetButtonDown("Jump") && isGrounded()) 
        {
            animator.SetTrigger("Jump");

            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        } 
        else if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f) 
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool isGrounded()
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
    }
}

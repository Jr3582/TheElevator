using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    public float sprintMultiplier = 1.5f;
    public float jumpingPower = 18f;
    private bool isFacingRight = true;

    public float maxStamina = 100f;
    private float currentStamina;
    public float walkStaminaDrain = 5f; // Per second
    public float sprintStaminaDrain = 15f; // Per second
    public float jumpStaminaCost = 20f; // Per jump
    public float staminaRegenRate = 10f; // Per second
    private bool isSprinting;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private RectTransform staminaBar;
    private float originalStaminaBarWidth;

    void Start()
    {
        currentStamina = maxStamina;

        originalStaminaBarWidth = staminaBar.sizeDelta.x;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        isSprinting = Input.GetKey(KeyCode.LeftShift) && currentStamina > 0;

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

            currentStamina -= jumpStaminaCost;
        } 
        else if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f) 
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (currentStamina < maxStamina && (horizontal == 0 || !isSprinting) && isGrounded())
        {
            currentStamina += staminaRegenRate * Time.deltaTime; // Regen stamina
        }
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        
        float staminaPercent = currentStamina / maxStamina;
        staminaBar.sizeDelta = new Vector2(originalStaminaBarWidth * staminaPercent, staminaBar.sizeDelta.y);

        Flip();
    }

    private void FixedUpdate()
    {
        float currentSpeed = isSprinting ? speed * sprintMultiplier : speed;
        rb.velocity = new Vector2(horizontal * currentSpeed, rb.velocity.y);

        if (horizontal != 0 && isGrounded())
        {
            float drainRate = isSprinting ? sprintStaminaDrain : walkStaminaDrain;
            currentStamina -= drainRate * Time.fixedDeltaTime;
        }

        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        Debug.Log(rb.velocity);
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

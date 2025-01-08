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
    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;
    public Vector2 crouchColliderSize = new Vector2(1f, 0.5f);
    public Vector2 crouchColliderOffset = new Vector2(0f, -0.25f);
    private bool isCrouching;
    public HungerBarScript hungerSystem;
    public float crouchSpeedMultiplier = 0.5f;
    public int maxJumps = 2;
    private int jumpCount;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D playerCollider;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private RectTransform staminaBar;
    private float originalStaminaBarWidth;

    void Start()
    {
        currentStamina = maxStamina;

        originalStaminaBarWidth = staminaBar.sizeDelta.x;

        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        playerCollider.sharedMaterial = Resources.Load<PhysicsMaterial2D>("LowFriction");
            
        originalColliderSize = playerCollider.size;

        originalColliderOffset = playerCollider.offset;

        jumpCount = maxJumps;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        isSprinting = Input.GetKey(KeyCode.LeftShift) && currentStamina >= 0;

        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        if (rb.velocity.y < 0 && !isGrounded()) {
            animator.SetBool("IsFalling", true);

        } else if (rb.velocity.y > 0) {
            animator.SetBool("IsFalling", false);

        } else if (isGrounded()) {
            animator.SetBool("IsGrounded", true);

            animator.SetBool("IsFalling", false);

            jumpCount = maxJumps;
        }

        if(Input.GetButtonDown("Jump") && currentStamina >= jumpStaminaCost && jumpCount > 0) 
        {
            Jump();

            currentStamina -= jumpStaminaCost;

            hungerSystem.DepleteHunger(5f); 
        } 
        else if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f) 
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (currentStamina < maxStamina && (horizontal == 0 || !isSprinting) && isGrounded())
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
        }

        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        
        float staminaPercent = currentStamina / maxStamina;
        staminaBar.sizeDelta = new Vector2(originalStaminaBarWidth * staminaPercent, staminaBar.sizeDelta.y);

        if (isSprinting || horizontal != 0)
        {
            float hungerDepletionRate = isSprinting ? 2f : 0.5f;
            hungerSystem.DepleteHunger(hungerDepletionRate * Time.deltaTime * 0.5f);
        }
        if (Input.GetKey(KeyCode.C))
        {
            isCrouching = true;
            animator.SetBool("IsCrouching", true);

            playerCollider.size = crouchColliderSize;
            playerCollider.offset = crouchColliderOffset;
        }
        else
        {
            isCrouching = false;
            animator.SetBool("IsCrouching", false);

            playerCollider.size = originalColliderSize;
            playerCollider.offset = originalColliderOffset;
        }

        Flip();
    }

    private void FixedUpdate()
    {    
        float currentSpeed = speed;

        if (isSprinting && currentStamina > 0)
        {
            currentSpeed *= sprintMultiplier;
        }
        else if (isCrouching)
        {
            currentSpeed *= crouchSpeedMultiplier;
        }

        rb.velocity = new Vector2(horizontal * currentSpeed, rb.velocity.y);

        if (horizontal != 0 && isGrounded())
        {
            float drainRate = isSprinting ? sprintStaminaDrain : walkStaminaDrain;
            currentStamina -= drainRate * Time.fixedDeltaTime;
        }

        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

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
    private void Jump() { 
        rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        animator.SetTrigger("Jump");

        jumpCount--;
    }
}

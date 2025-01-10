using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Player's transform
    public float speed = 2f; // Speed while chasing
    public float chaseSpeed = 4f; // Speed when chasing
    public float detectionRadius = 5f; // Radius for detecting the player
    public LayerMask playerLayer; // Layer to detect the player
    public float attackCooldown = 1f; // Cooldown time between attacks

    private Rigidbody2D rb;
    private Animator animator; // Animator reference for controlling animations
    private bool isChasing = false; // Whether the enemy is chasing the player
    private bool isAttacking = false; // Whether the enemy is attacking
    private float currentAttackCooldown = 0f; // Tracks the cooldown between attacks

    public CircleCollider2D attackCollider; // Public CircleCollider2D for the attack range
    private Collider2D playerCollider;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Initialize animator

        if (attackCollider == null)
        {
            attackCollider = GetComponent<CircleCollider2D>(); // If not assigned, attempt to get the CircleCollider2D
        }
    }

    private void Update()
    {
        // Check if the enemy is in range of the player
        if (Vector2.Distance(transform.position, player.position) <= detectionRadius)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        if (isChasing)
        {
            // Trigger the awake animation when the enemy detects the player for the first time
            PlayAwakeAnimation();

            // Move towards the player
            MoveTowards(player.position, chaseSpeed);
        }
        else
        {
            // Stay idle if not chasing the player
            StayIdle();
        }

        // Check if the player is within attack range and the cooldown is finished
        if (isChasing && !isAttacking && playerCollider != null && attackCollider.IsTouching(playerCollider) && currentAttackCooldown <= 0)
        {
            // If the player is inside the collider, attack
            AttackPlayer(playerCollider.gameObject);
        }


        // Handle attack cooldown
        if (currentAttackCooldown > 0)
        {
            currentAttackCooldown -= Time.deltaTime;
        }
    }

    private void StayIdle()
    {
        rb.velocity = Vector2.zero; // Stop all movement
        SetIdleAnimation(); // Set idle animation when stationary
    }

    private void MoveTowards(Vector2 target, float moveSpeed)
    {
        Vector2 direction = (target - (Vector2)transform.position);
        direction.y = 0;  // Ignore vertical movement
        direction = direction.normalized;

        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

        // Flip the enemy to face the player
        FlipDirection(direction.x);

        SetWalkAnimation(); // Set walking animation while moving
    }

    private void FlipDirection(float directionX)
    {
        // Flip the enemy's facing direction by scaling the X axis
        Vector3 localScale = transform.localScale;

        if (directionX < 0 && localScale.x > 0)
        {
            localScale.x = -localScale.x; // Flip to the left
        }
        else if (directionX > 0 && localScale.x < 0)
        {
            localScale.x = -localScale.x; // Flip to the right
        }

        transform.localScale = localScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCollider = other;
            if (currentAttackCooldown <= 0)
            {
                SetAttackAnimation();
                AttackPlayer(other.gameObject);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCollider = null;
        }
    }

    // Function to apply damage to the player (you can customize this)
    private void AttackPlayer(GameObject player)
    {
        // Assuming you have a PlayerHealth script that handles the player's health
        HealthScript playerHealth = player.GetComponent<HealthScript>();
        if (playerHealth != null)
        {
            playerHealth.DepleteHealth(0.5f); // Apply damage to the player (change as needed)
        }

        // Reset the attack cooldown
        currentAttackCooldown = attackCooldown;
    }

    private void PlayAwakeAnimation()
    {
        animator.SetTrigger("Awake"); // Trigger the awake animation when the enemy detects the player
    }

    private void SetIdleAnimation()
    {
        animator.SetBool("IsWalking", false); // Ensure walking animation is off
        animator.SetBool("IsIdle", true); // Set idle animation on
    }

    private void SetWalkAnimation()
    {
        animator.SetBool("IsWalking", true); // Set walking animation on
        animator.SetBool("IsIdle", false); // Ensure idle animation is off
    }

    private void SetAttackAnimation()
    {
        animator.SetTrigger("Attack"); // Trigger the attack animation
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize detection radius and attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        // Visualize ground check
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 0.2f);
    }
}

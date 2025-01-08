using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float roamSpeed = 2f; // Speed when roaming
    public float chaseSpeed = 4f; // Speed when chasing the player
    public float detectionRange = 10f; // Range within which the enemy can detect the player
    public float fieldOfViewAngle = 110f; // Field of view for the enemy (cone angle)
    public Transform player; // Reference to the player object
    public Transform[] patrolPoints; // Array of patrol points
    private int currentPatrolPoint = 0;
    private NavMeshAgent agent; // Reference to the NavMeshAgent for movement

    private bool isChasing = false; // Flag to check if the enemy is chasing the player

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = roamSpeed;
        if (patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[currentPatrolPoint].position);
        }
    }

    void Update()
    {
        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Roam();
        }

        DetectPlayer();
    }

    // Roaming behavior when the player is not detected
    private void Roam()
    {
        if (agent.remainingDistance < 0.5f) // If close to the current patrol point
        {
            currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPatrolPoint].position);
        }
    }

    // Chase the player if within sight
    private void ChasePlayer()
    {
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);
    }

    // Check if the player is within sight
    private void DetectPlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float angle = Vector3.Angle(directionToPlayer, transform.forward);

        if (angle < fieldOfViewAngle / 2f && directionToPlayer.magnitude < detectionRange)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToPlayer.normalized, out hit, detectionRange))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    isChasing = true; // Start chasing the player
                }
            }
        }
        else
        {
            isChasing = false; // Stop chasing if the player is out of sight
        }
    }

    // Optional: Attack behavior when close to the player (e.g., when in range)
    private void AttackPlayer()
    {
        // Attack logic (e.g., decrease player's health)
        Debug.Log("Enemy is attacking the player!");
    }
}

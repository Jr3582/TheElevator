using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float roamSpeed = 2f;
    public float chaseSpeed = 4f;
    public float detectionRange = 10f;
    public float fieldOfViewAngle = 110f;
    public Transform player;
    public Transform[] patrolPoints;
    public float groundCheckDistance = 1f; // Distance to check for ground
    public LayerMask groundLayer; // Layer to identify ground

    private int currentPatrolPoint = 0;
    private NavMeshAgent agent;
    private bool isChasing = false;

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
            if (CanMoveForward())
                ChasePlayer();
        }
        else
        {
            if (CanMoveForward())
                Roam();
        }

        DetectPlayer();
    }

    private void Roam()
    {
        if (agent.remainingDistance < 0.5f)
        {
            currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPatrolPoint].position);
        }
    }

    private void ChasePlayer()
    {
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);
    }

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
                    isChasing = true;
                }
            }
        }
        else
        {
            isChasing = false;
        }
    }

    // Check if there's ground ahead before moving
    private bool CanMoveForward()
    {
        Vector3 rayOrigin = transform.position + Vector3.forward * agent.radius;
        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, groundCheckDistance, groundLayer))
        {
            Debug.DrawRay(rayOrigin, Vector3.down * groundCheckDistance, Color.green);
            return true; // Ground detected
        }

        Debug.DrawRay(rayOrigin, Vector3.down * groundCheckDistance, Color.red);
        return false; // No ground detected
    }
}

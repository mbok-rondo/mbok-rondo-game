using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLogic : MonoBehaviour
{
    [Header("Patrol Settings")]
    public float patrolRadius = 5f;
    public float walkDuration = 3f;
    public float idleDuration = 2f;
    public float patrolSpeed = 2f;

    private NavMeshAgent agent;
    private Animator animator;
    private Vector3 targetPosition;

    private bool isWalking = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.speed = patrolSpeed;
        agent.stoppingDistance = 0.1f;

        StartCoroutine(PatrolRoutine());
    }

    void Update()
    {
        // Update animasi berdasarkan kecepatan agent
        if (agent.velocity.magnitude > 0.1f)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }

    IEnumerator PatrolRoutine()
    {
        while (true)
        {
            // Dapatkan posisi acak di dalam radius
            Vector2 randomCircle = Random.insideUnitCircle * patrolRadius;
            targetPosition = transform.position + new Vector3(randomCircle.x, 0f, randomCircle.y);

            NavMeshHit hit;
            if (NavMesh.SamplePosition(targetPosition, out hit, patrolRadius, NavMesh.AllAreas))
            {
                targetPosition = hit.position;
                agent.SetDestination(targetPosition);
            }

            // Jalan selama beberapa detik
            isWalking = true;
            yield return new WaitForSeconds(walkDuration);

            // Berhenti jalan
            agent.ResetPath();
            isWalking = false;
            yield return new WaitForSeconds(idleDuration);
        }
    }
}

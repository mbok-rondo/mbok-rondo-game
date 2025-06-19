using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLogic : MonoBehaviour
{
    public Transform player;
    public float chaseRange = 10f;
    public float attackRange = 2f;
    public float timeBetweenAttacks = 1.5f;
    public float patrolRadius = 10f;
    public LayerMask obstacleMask;

    private NavMeshAgent agent;
    private Animator animator;
    private bool alreadyAttacked;
    private bool playerDead;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        GoToRandomPatrolPoint();
    }

    void Update()
    {
        if (playerDead) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (CanSeePlayer() && distanceToPlayer <= chaseRange)
        {
            ChasePlayer();

            if (distanceToPlayer <= attackRange)
            {
                AttackPlayer();
            }
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        animator.SetBool("Walk", true);
        animator.SetBool("Run", false);

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToRandomPatrolPoint();
        }
    }

    void GoToRandomPatrolPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

void ChasePlayer()
{
    Debug.Log("Chasing player. Setting Run to true.");
    animator.SetBool("Walk", false);
    animator.SetBool("Run", true);
    agent.SetDestination(player.position);
}


    void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        animator.SetBool("Walk", false);
        animator.SetBool("Run", false);
        animator.SetTrigger("Attack");

        if (!alreadyAttacked)
        {
            playerDead = true;
            Debug.Log("MC died.");
            // player.GetComponent<PlayerHealth>()?.Die();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    bool CanSeePlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, player.position);

        if (!Physics.Raycast(transform.position, directionToPlayer, distance, obstacleMask))
        {
<<<<<<< Updated upstream
            return true;
=======
                if (target == null) return;
    if (target.name == "SoundTarget") return; // hindari menyerang dummy
            isInvestigatingItem = false;
            isWaitingAtItem = false;
            isPatrolling = false;

            agent.ResetPath();
            // transform.LookAt(player);
            transform.LookAt(target);


            animator.SetBool("Run", false);
            animator.SetBool("Walk", false);
            animator.SetBool("Attack", true);

            Debug.Log("Enemy attacking player - Animation: Attack");
            
            GameManager.instance.TriggerDefeat();

>>>>>>> Stashed changes
        }
        return false;
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }
}

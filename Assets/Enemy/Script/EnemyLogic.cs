using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLogic : MonoBehaviour
{
    public Transform[] patrolPoints;
    private int patrolIndex;

    public Transform player;
    public float chaseDistance = 10f;
    public float attackDistance = 2f;
    public float itemDetectRadius = 50f;

    public float walkspeed, runspeed;

    private NavMeshAgent agent;
    private Animator animator;

    private GameObject targetItem;
    private bool isInvestigatingItem = false;
    private bool isWaitingAtItem = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        patrolIndex = 0;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        bool canSeePlayer = CanSeePlayer();

        // Prioritaskan chase jika melihat player kapanpun
        if (canSeePlayer && distanceToPlayer <= attackDistance)
        {
            StopAllCoroutines(); // hentikan investigasi jika sedang berjalan
            HandleAttack();
            return;
        }
        else if (canSeePlayer && distanceToPlayer <= chaseDistance)
        {
            StopAllCoroutines();
            HandleChase();
            return;
        }

        // Jika sedang mengejar atau menunggu di item
        if (isInvestigatingItem)
        {
            HandleInvestigateItem();
            return;
        }

        // Cek item
        if (CheckForThrowableItem())
        {
            HandleInvestigateItem();
        }
        else
        {
            HandlePatrol();
        }
    }

    private void HandlePatrol()
    {
        agent.isStopped = false;
        if (patrolPoints.Length == 0) return;

        agent.SetDestination(patrolPoints[patrolIndex].position);

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        }

        animator.SetBool("Run", false);
        animator.SetBool("Attack", false);
    }

    private void HandleChase()
    {
        isInvestigatingItem = false;
        isWaitingAtItem = false;

        agent.isStopped = false;
        agent.SetDestination(player.position);

        animator.SetBool("Run", true);
        animator.SetBool("Attack", false);
    }

    private void HandleAttack()
    {
        isInvestigatingItem = false;
        isWaitingAtItem = false;

        agent.ResetPath();
        transform.LookAt(player);

        animator.SetBool("Run", false);
        animator.SetBool("Attack", true);
    }

    private void HandleInvestigateItem()
    {
        if (targetItem == null)
        {
            isInvestigatingItem = false;
            return;
        }

        if (!isWaitingAtItem)
        {
            agent.isStopped = false;
            agent.SetDestination(targetItem.transform.position);

            animator.SetBool("Run", true);
            animator.SetBool("Attack", false);

            if (!agent.pathPending && agent.remainingDistance < 1.0f)
            {
                // Sudah sampai item, mulai tunggu 5 detik
                StartCoroutine(InvestigateItemCoroutine());
            }
        }
    }

    private IEnumerator InvestigateItemCoroutine()
    {
        isWaitingAtItem = true;
        agent.isStopped = true;
     //   animator.SetBool("Walk", false);
        Debug.Log("waiting....");
        animator.SetBool("Wait", true);
        yield return new WaitForSeconds(5f);

        isInvestigatingItem = false;
        isWaitingAtItem = false;
        targetItem = null;
    }

    private bool CheckForThrowableItem()
    {
        GameObject[] allItems = GameObject.FindGameObjectsWithTag("Throwable");

        foreach (GameObject item in allItems)
        {
            // Cek apakah item sudah pernah diinvestigasi
            ThrowableTracker tracker = item.GetComponent<ThrowableTracker>();
            if (tracker != null && tracker.isInvestigated)
                continue;

            // Cek apakah menyentuh tanah
            if (Physics.Raycast(item.transform.position, Vector3.down, out RaycastHit groundHit, 2f))
            {
                if (groundHit.collider.CompareTag("Ground"))
                {
                    targetItem = item;
                    isInvestigatingItem = true;

                    // Tandai bahwa item ini sedang diinvestigasi
                    if (tracker != null)
                        tracker.isInvestigated = true;

                    return true;
                }
            }
        }

        return false;
    }


    private bool CanSeePlayer()
    {
        Ray ray = new Ray(transform.position + Vector3.up, (player.position - transform.position).normalized);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, chaseDistance))
        {
            if (hit.transform == player)
            {
                return true;
            }
        }
        return false;
    }
}

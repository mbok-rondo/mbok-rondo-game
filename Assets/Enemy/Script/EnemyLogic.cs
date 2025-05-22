using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLogic : MonoBehaviour
{
    public Transform[] patrolPoints;
    private int patrolIndex;

    public Transform player;
    public float chaseDistance = 40f;
    public float attackDistance = 18;
    public float itemDetectRadius = 100f;

    public float walkspeed, runspeed;

    private NavMeshAgent agent;
    private Animator animator;

    private GameObject targetItem;
    private bool isInvestigatingItem = false;
    private bool isWaitingAtItem = false;
    private bool isPatrolling = false; // ✅ Flag baru ditambahkan di sini

    [Header("Enemy SFX")]
    public AudioClip StepAudio;
    AudioSource EnemyAudio;
    public AudioClip RunAudio;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        patrolIndex = 0;
            EnemyAudio = GetComponent<AudioSource>(); // ✅ tambahkan baris ini

    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        bool canSeePlayer = CanSeePlayer();
// Debug.Log($"Anim Params - Walk: {animator.GetBool("Walk")}, Run: {animator.GetBool("Run")}, Attack: {animator.GetBool("Attack")}, Wait: {animator.GetBool("Wait")}");

        // Prioritaskan chase jika melihat player kapanpun
        if (canSeePlayer && distanceToPlayer <= attackDistance)
        {
            StopAllCoroutines();
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
        // Cegah patrol jika sedang mengerjakan hal lain
        if (isInvestigatingItem || isWaitingAtItem)
        {
            isPatrolling = false;
            animator.SetBool("Walk", false);
            return;
        }

        isPatrolling = true;

        if (patrolPoints.Length == 0) return;

        agent.isStopped = false;
        agent.SetDestination(patrolPoints[patrolIndex].position);

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        }

        animator.SetBool("Run", false);
        animator.SetBool("Attack", false);
        animator.SetBool("Walk", true);

        Debug.Log("Enemy patrolling - Animation: Walk");
    }

    private void HandleChase()
    {
        isInvestigatingItem = false;
        isWaitingAtItem = false;
        isPatrolling = false;

        agent.isStopped = false;
        agent.SetDestination(player.position);

        animator.SetBool("Run", true);
        animator.SetBool("Attack", false);
        animator.SetBool("Walk", false);

        Debug.Log("Enemy chasing player - Animation: Run");
    }

    private void HandleAttack()
    {
        isInvestigatingItem = false;
        isWaitingAtItem = false;
        isPatrolling = false;

        agent.ResetPath();
        transform.LookAt(player);

        animator.SetBool("Run", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Attack", true);

        Debug.Log("Enemy attacking player - Animation: Attack");
    }

    private void HandleInvestigateItem()
    {
        isPatrolling = false;

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
            animator.SetBool("Walk", false);

            if (!agent.pathPending && agent.remainingDistance < 1.0f)
            {
                StartCoroutine(InvestigateItemCoroutine());
            }
        }
    }

    private IEnumerator InvestigateItemCoroutine()
    {
        isWaitingAtItem = true;
        isPatrolling = false;

        agent.isStopped = true;

        animator.SetBool("Wait", true);
        animator.SetBool("Walk", false);
        animator.SetBool("Run", false);

        Debug.Log("Enemy investigating item - Animation: Wait");

        yield return new WaitForSeconds(5f);

        isInvestigatingItem = false;
        isWaitingAtItem = false;
        targetItem = null;

        animator.SetBool("Wait", false);
    }

    private bool CheckForThrowableItem()
    {
        GameObject[] allItems = GameObject.FindGameObjectsWithTag("Throwable");

        foreach (GameObject item in allItems)
        {
            ThrowableTracker tracker = item.GetComponent<ThrowableTracker>();
            if (tracker != null && tracker.isInvestigated)
                continue;

            if (Physics.Raycast(item.transform.position, Vector3.down, out RaycastHit groundHit, 2f))
            {
                if (groundHit.collider.CompareTag("Ground"))
                {
                    targetItem = item;
                    isInvestigatingItem = true;

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
                    Debug.Log("Raycast hit: " + hit.transform.name);

            if (hit.transform == player)
            {
                return true;
            }
        }
        return false;
    }
        private void step(){
        Debug.Log("step");
        EnemyAudio.clip = StepAudio;
        EnemyAudio.Play();
    }
    private void run(){
        Debug.Log("run");
        EnemyAudio.clip = RunAudio;
        EnemyAudio.Play();
    }
}

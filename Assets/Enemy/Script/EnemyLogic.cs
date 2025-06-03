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
        public float itemDetectRadius = 50f;

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

        private Transform soundTarget;
        private bool isTrackingSound = false;
        public float forgetSoundDistance = 30f; // Jarak untuk berhenti mengejar

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

            if (isTrackingSound && soundTarget != null)
            {
                float distanceToTarget = Vector3.Distance(transform.position, soundTarget.position);

                Debug.DrawLine(transform.position, soundTarget.position, Color.black);
                // Debug.Log($"[Track] {gameObject.name} => DistanceToTarget: {distanceToTarget}, forgetSoundDistance: {forgetSoundDistance}");

                // ✅ Tambahkan pengecekan ini
                if (!agent.pathPending && agent.remainingDistance < 1f)
                {
                    if (soundTarget.name == "SoundTarget") // dummy object
                    {
                        Debug.Log($"Enemy {gameObject.name} sampai ke dummy target, balik patrol");

                        Destroy(soundTarget.gameObject);
                        soundTarget = null;
                        isTrackingSound = false;

                        agent.ResetPath();
                        animator.SetBool("Run", false);
                        animator.SetBool("Attack", false);
                        animator.SetBool("Wait", false);
                        animator.SetBool("Walk", false);

                        StartCoroutine(ReturnToPatrolWithDelay());
                        return;
                    }
                }

                if (distanceToTarget > forgetSoundDistance)
                {
                    Debug.Log($"Enemy {gameObject.name} LUPA suara karena terlalu jauh");

                    if (soundTarget != null && soundTarget.name == "SoundTarget")
                        Destroy(soundTarget.gameObject);

                    soundTarget = null;
                    isTrackingSound = false;

                    agent.ResetPath();
                    isInvestigatingItem = false;
                    isWaitingAtItem = false;
                    isPatrolling = false;

                    animator.SetBool("Run", false);
                    animator.SetBool("Attack", false);
                    animator.SetBool("Wait", false);
                    animator.SetBool("Walk", false);

                    StartCoroutine(ReturnToPatrolWithDelay());
                    return;
                }


                else if (distanceToTarget <= attackDistance)
                {
                    if (soundTarget == player)
                    {
                        Debug.Log($"Enemy {gameObject.name} menyerang PLAYER");
                        HandleAttack(soundTarget);
                    }
                }
                else
                {
                    HandleChase(soundTarget);
                }

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
        private IEnumerator ReturnToPatrolWithDelay()
        {
            yield return new WaitForSeconds(0.2f); // kasih jeda 1–2 frame

            isPatrolling = true;
            animator.SetBool("Walk", true);
            HandlePatrol();
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

            // ➕ Tambahkan ini agar path lama dihapus
            agent.ResetPath();

            // ➕ Set destinasi ulang ke patrol point
            agent.SetDestination(patrolPoints[patrolIndex].position);

            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
                agent.SetDestination(patrolPoints[patrolIndex].position); // Pastikan tujuan baru diatur
            }

            animator.SetBool("Run", false);
            animator.SetBool("Attack", false);
            animator.SetBool("Walk", true);

            // Debug.Log("Enemy patrolling - Animation: Walk");
        }


        private void HandleChase(Transform target)
        {
            isInvestigatingItem = false;
            isWaitingAtItem = false;
            isPatrolling = false;

            agent.isStopped = false;
            // agent.SetDestination(player.position);
            agent.isStopped = false;

            if (!agent.hasPath || agent.remainingDistance > 0.5f)
            {
                agent.SetDestination(target.position);
            }

            animator.SetBool("Run", true);
            animator.SetBool("Attack", false);
            animator.SetBool("Walk", false);

            // Debug.Log("Enemy chasing player - Animation: Run");
        }

        private void HandleAttack(Transform target)
        {
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
            isPatrolling = true;
            HandlePatrol(); // atau biarkan update() mengatur
        }

        private bool CheckForThrowableItem()
        {
            GameObject[] allItems = GameObject.FindGameObjectsWithTag("Throwable");

            foreach (GameObject item in allItems)
            {
                ThrowableTracker tracker = item.GetComponent<ThrowableTracker>();
                if (tracker != null && tracker.isInvestigated)
                    continue;

                float distance = Vector3.Distance(transform.position, item.transform.position);
                if (distance <= itemDetectRadius)
                {
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
            }

            return false;
        }

        // private bool CanSeePlayer()
        // {
        //     Ray ray = new Ray(transform.position + Vector3.up, (player.position - transform.position).normalized);
        //     RaycastHit hit;

        //     if (Physics.Raycast(ray, out hit, chaseDistance))
        //     {
        //                 Debug.Log("Raycast hit: " + hit.transform.name);

        //         if (hit.transform == player)
        //         {
        //             return true;
        //         }
        //     }
        //     return false;
        // }
            private void step(){
            // Debug.Log("step");
            EnemyAudio.clip = StepAudio;
            EnemyAudio.Play();
        }
        private void run(){
            // Debug.Log("run");
            EnemyAudio.clip = RunAudio;
            EnemyAudio.Play();
        }

    public void OnHearSound(Vector3 soundPosition, bool isPlayerSound, float soundRadius)
    {
            float distanceToSound = Vector3.Distance(transform.position, soundPosition);

        if (isPlayerSound)
        {
            // Destroy soundTarget sebelumnya biar gak numpuk dummy
            if (soundTarget != null && soundTarget.name == "SoundTarget")
                Destroy(soundTarget.gameObject);

            GameObject tempTarget = new GameObject("SoundTarget");
            tempTarget.transform.position = soundPosition;
            Destroy(tempTarget, 5f); // auto destroy dalam 5 detik

            soundTarget = tempTarget.transform;
            isTrackingSound = true;

            Debug.Log($"{gameObject.name} mendengar suara PLAYER di posisi {soundPosition}");
        }
        else
        {
            // float distanceToSound = Vector3.Distance(transform.position, soundPosition);

            // GANTI dari ini:
            // if (distanceToSound <= itemDetectRadius)

            // JADI ini:
            if (distanceToSound <= soundRadius)
            {
                GameObject nearestItem = FindNearestThrowableAt(soundPosition);
                if (nearestItem != null)
                {
                    targetItem = nearestItem;
                    isInvestigatingItem = true;

                    ThrowableTracker tracker = nearestItem.GetComponent<ThrowableTracker>();
                    if (tracker != null)
                        tracker.isInvestigated = true;

                    Debug.Log($"{gameObject.name} denger suara ITEM di {soundPosition}, investigasi: {nearestItem.name}");
                }
            }
        }
    }


    
    private GameObject FindNearestThrowableAt(Vector3 soundPosition)
    {
        GameObject[] allItems = GameObject.FindGameObjectsWithTag("Throwable");
        GameObject nearest = null;
        float minDist = Mathf.Infinity;

        foreach (GameObject item in allItems)
        {
            float dist = Vector3.Distance(item.transform.position, soundPosition);
            if (dist < 3f && dist < minDist) // radius suara bisa diatur
            {
                minDist = dist;
                nearest = item;
            }
        }

        return nearest;
    }
    private void OnDrawGizmosSelected()
    {
        // Radius suara jalan (misalnya 10)
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 10f);

        // Radius suara lari (misalnya 20)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 20f);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 30f);
        
        // Radius deteksi item lemparan
        // Gizmos.color = Color.black;
        // Gizmos.DrawWireSphere(transform.position, 40f);
            // Radius deteksi suara bowl/item
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position, itemDetectRadius);

    }

    }

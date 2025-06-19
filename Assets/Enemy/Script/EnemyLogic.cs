using System.Collections;
    using UnityEngine;
    using UnityEngine.AI;
    using System.Collections.Generic;

    public class EnemyLogic : MonoBehaviour
    {
        public Transform[] patrolPoints;
        private int patrolIndex;

        [Header("Enemy Radius Distance")]
        public Transform player;
        public float chaseDistance = 40f;
        public float attackDistance = 18;
        public float itemDetectRadius;

        public float walkspeed, runspeed;

        private NavMeshAgent agent;
        private Animator animator;

        private GameObject targetItem;
        private bool isInvestigatingItem = false;
        private bool isWaitingAtItem = false;
        private bool isPatrolling = false; // ✅ Flag baru ditambahkan di sini
        [SerializeField] private LayerMask wallMask;

        [Header("Enemy SFX")]
        public AudioClip StepAudio;
        AudioSource EnemyAudio;
        public AudioClip RunAudio;

        private Transform soundTarget;
        private bool isTrackingSound = false;
        public float forgetSoundDistance; // Jarak untuk berhenti mengejar
        private float lastHeardSoundRadius;  // untuk gizmo putih

        [Header("Enemy SFX For SPK")]
        private List<SoundEvent> soundEvents = new List<SoundEvent>();

        private class SoundEvent{
            public Vector3 position;
            public float radius;
            public bool isPlayer;
            public float timestamp;

            public SoundEvent(Vector3 pos, float rad, bool isPlayer){
                this.position = pos;
                this.radius = rad;
                this.isPlayer = isPlayer;
                this.timestamp = Time.time;
            }
        }

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
            if (target == null) return;
            if (target.name == "SoundTarget") return; // hindari menyerang dummy

            isInvestigatingItem = false;
            isWaitingAtItem = false;
            isPatrolling = false;

            agent.ResetPath();
            transform.LookAt(target);

            animator.SetBool("Run", true);
            animator.SetBool("Walk", false);
            animator.SetBool("Attack", false);

            if (!agent.hasPath || agent.remainingDistance > 0.5f)
            {
                agent.SetDestination(target.position);
            }

            Debug.Log("Enemy chasing target - Animation: Run");
        }


        private void HandleAttack(Transform target)
        {
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
            float dist = Vector3.Distance(transform.position, soundPosition);

            // 💡 Gunakan pengurangan radius akibat tembok
            float effectiveRadius = ApplyOcclusionReduction(soundPosition, soundRadius);

            if (dist <= effectiveRadius)  // ✔ GUNAKAN radius hasil reduksi
            {
                // Tambahkan suara ke daftar event
                soundEvents.Add(new SoundEvent(soundPosition, soundRadius, isPlayerSound));

                // Evaluasi suara terbaik setelah update
                EvaluateBestSoundTarget();
            }
        }


        private void EvaluateBestSoundTarget(){
            if (soundEvents.Count == 0) return;

            //hapus suara lama
            soundEvents.RemoveAll(e => Time.time - e.timestamp > 5f);

            SoundEvent closest = null;
            float minDist = Mathf.Infinity;

            foreach (var se in soundEvents){
                float dist = Vector3.Distance(transform.position, se.position);
                if (dist < minDist){
                    minDist = dist;
                    closest = se;
                }
            }

            if (closest == null) return;
            lastHeardSoundRadius = closest.radius; // simpan radius asli suara

            float currentTargetDist = (soundTarget != null) ? Vector3.Distance(transform.position, soundTarget.position) : Mathf.Infinity;

            if(soundTarget == null || minDist <currentTargetDist){
                if(soundTarget != null && soundTarget.name == "SoundTarget")
                Destroy(soundTarget.gameObject);

            if (closest.isPlayer)
            {
                soundTarget = player; // <- langsung pakai player
            }
            else
            {
                GameObject tempTarget = new GameObject("SoundTarget");
                tempTarget.transform.position = closest.position;
                Destroy(tempTarget, 5f);
                soundTarget = tempTarget.transform;
            }

                isTrackingSound = true;
                
                Debug.Log($"[SPK] {gameObject.name} mengejar suara terdekat di {closest.position}, jarak {minDist}");
        // Kosongkan list agar tidak evaluasi ulang untuk suara yang sama

                soundEvents.Clear();

            }
        }
        
        private float ApplyOcclusionReduction(Vector3 sourcePos, float originalRadius)
        {
            Vector3 direction = (transform.position - sourcePos).normalized;
            float distance = Vector3.Distance(sourcePos, transform.position);

            int wallCount = 0;
            Ray ray = new Ray(sourcePos, direction);
            RaycastHit[] hits = new RaycastHit[10];
            int hitCount = Physics.RaycastNonAlloc(ray, hits, distance, wallMask);

            for (int i = 0; i < hitCount; i++)
            {
                if (hits[i].collider != null)
                {
                    wallCount++;
                    Debug.DrawLine(sourcePos, hits[i].point, Color.magenta, 1f);
                }
            }

            float reductionPerWall = 0.4f;
            float reducedRadius = originalRadius * Mathf.Pow(1f - reductionPerWall, wallCount);

            Debug.DrawLine(sourcePos, transform.position, wallCount > 0 ? Color.gray : Color.cyan, 1f);
            return reducedRadius;
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
            Gizmos.DrawWireSphere(transform.position, 15f);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, forgetSoundDistance);
            
            // Radius deteksi item lemparan
            // Gizmos.color = Color.black;
            // Gizmos.DrawWireSphere(transform.position, 40f);
                // Radius deteksi suara bowl/item
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, itemDetectRadius);

            #if UNITY_EDITOR
            if (Application.isPlaying && soundTarget != null)
            {
                float dynamicRadius = ApplyOcclusionReduction(soundTarget.position, lastHeardSoundRadius);
                Gizmos.color = Color.white;
                Gizmos.DrawWireSphere(transform.position, dynamicRadius);

                UnityEditor.Handles.Label(transform.position + Vector3.up * 2f, $"Hearing Range: {dynamicRadius:F1}");
            }
            #endif

}
}
        using UnityEngine;

        [RequireComponent(typeof(AudioSource))]
        public class BowlSound : MonoBehaviour
        {
            public AudioClip impactSound;
            private AudioSource audioSource;
            public float soundRadius = 50f;
            private bool hasPlayed = false;

            void Start()
            {
                audioSource = GetComponent<AudioSource>();
            }

            void OnCollisionEnter(Collision collision)
            {
                if (!hasPlayed && collision.relativeVelocity.magnitude > 2f)
                {
                            Debug.Log($"BOWL collided! velocity: {collision.relativeVelocity.magnitude}");

                    audioSource.clip = impactSound;
                    audioSource.Play();
                    hasPlayed = true;

                    SoundEmitter emitter = GetComponent<SoundEmitter>();
                    if (emitter != null)
                    {
                        emitter.EmitSound(soundRadius, false); // FALSE = bukan player
                        TestEnemyDistance();

                    }

                    Invoke(nameof(ResetPlay), 0.1f);
                }
            }
            private void TestEnemyDistance()
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); // tambahkan tag "Enemy" di prefab
                foreach (var enemy in enemies)
                {
                    float dist = Vector3.Distance(transform.position, enemy.transform.position);
                    Debug.Log($"Jarak Bowl ke {enemy.name}: {dist}");
                }
            }

            void ResetPlay()
            {
                hasPlayed = false;
            }
            private void OnDrawGizmos()
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(transform.position, soundRadius);
            }

        }

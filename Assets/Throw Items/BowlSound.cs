using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BowlSound : MonoBehaviour
{
    public AudioClip impactSound;
    private AudioSource audioSource;

    private bool hasPlayed = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Opsional: Cegah bunyi saat dilempar ke player sendiri atau saat dipegang
        if (!hasPlayed && collision.relativeVelocity.magnitude > 2f)
        {
            audioSource.clip = impactSound;
            audioSource.Play();
            hasPlayed = true;

            // Optional: Reset agar bisa bunyi lagi jika dilempar berkali-kali
            Invoke(nameof(ResetPlay), 0.1f);
        }
    }

    void ResetPlay()
    {
        hasPlayed = false;
    }
}

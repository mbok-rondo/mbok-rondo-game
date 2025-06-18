using UnityEngine;

public class SoundEmitterRuller : MonoBehaviour
{
    public float soundRadius = 10f;

    public void EmitSound()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, soundRadius);
        foreach (var hit in hits)
        {
            EnemyHearing enemy = hit.GetComponent<EnemyHearing>();
            if (enemy != null)
            {
                enemy.OnHearSound(transform.position);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, soundRadius);
    }
}

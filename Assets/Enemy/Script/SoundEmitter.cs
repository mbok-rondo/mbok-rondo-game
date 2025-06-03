using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    public float soundRadius = 50f; // Bisa disesuaikan tergantung lari/jalan

    public void EmitSound(float radius, bool isPlayerSound)
    {
        // Debug.Log($"EmitSound dipanggil dari {gameObject.name}, radius: {radius}, isPlayerSound: {isPlayerSound}");
        Debug.Log($"[SOUND EMITTER] EmitSound dari {gameObject.name}, radius: {radius}, isPlayerSound: {isPlayerSound}");

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemyGO in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemyGO.transform.position);
            if (dist <= radius)
            {
                EnemyLogic enemy = enemyGO.GetComponent<EnemyLogic>();
                if (enemy != null)
                {
                    enemy.OnHearSound(transform.position, isPlayerSound,radius);
                }
            }
        }

    }

}

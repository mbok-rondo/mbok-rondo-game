using UnityEngine;

public class Target : MonoBehaviour
{
    public void StartDespawnTimer()
    {
        Destroy(gameObject, 30f); // Hancurkan objek setelah 3 detik
    }
}

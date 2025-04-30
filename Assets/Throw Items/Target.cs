using UnityEngine;

public class Target : MonoBehaviour
{
    public void StartDespawnTimer()
    {
        Destroy(gameObject, 3f); // Hancurkan objek setelah 3 detik
    }
}

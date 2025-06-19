using UnityEngine;

public class Key : MonoBehaviour
{
    // Opsional: tambahkan efek partikel atau suara saat kunci diambil
    public GameObject pickupEffect;
    public AudioClip pickupSound;

    private void OnTriggerEnter(Collider other)
    {
        // Cek apakah yang menyentuh adalah objek dengan tag "Player"
        if (other.CompareTag("Player"))
        {
            // Panggil fungsi untuk menambah kunci di GameManager
            GameManager.instance.CollectKey();

            // Mainkan suara di posisi kunci (jika ada)
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }

            // Tampilkan efek partikel di posisi kunci (jika ada)
            if (pickupEffect != null)
            {
                Instantiate(pickupEffect, transform.position, Quaternion.identity);
            }

            // Hancurkan objek kunci ini agar tidak bisa diambil lagi
            Destroy(gameObject);
        }
    }
}
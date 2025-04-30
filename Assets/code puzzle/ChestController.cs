using UnityEngine;

public class ChestController : MonoBehaviour
{
    public GameObject chestClosed;  // Referensi ke objek ChestClosed
    public GameObject chestOpen;    // Referensi ke objek ChestOpen
    public GameObject padlock;
    public GameObject RustKey;       // Referensi ke padlock

    public PadlockController padlockController; // Tambahkan referensi ke PadlockController

    // Fungsi untuk membuka peti jika kode benar
    public void OpenChest()
    {
        if (padlockController != null && padlockController.isUnlocked) // Cek apakah padlockController ada dan padlock sudah dibuka
        {
            chestClosed.SetActive(false);  // Sembunyikan peti tertutup
            chestOpen.SetActive(true);     // Tampilkan peti terbuka
            RustKey.SetActive(true);       // Tampilkan RustKey (kunci)

            padlock.SetActive(false);      // Sembunyikan padlock
            Debug.Log("Chest opened!");
        }
        else
        {
            Debug.Log("Padlock belum terbuka. Peti tidak bisa dibuka.");
        }
    }
}

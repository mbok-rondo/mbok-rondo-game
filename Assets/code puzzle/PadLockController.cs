using UnityEngine;

public class PadlockController : MonoBehaviour
{
    public GameObject chestClosed;  // Referensi ke objek ChestClosed
    public GameObject chestOpen;    // Referensi ke objek ChestOpen
    public GameObject padlock;
    public GameObject RustKey;      // Referensi ke padlock (yang mengontrol peti terbuka)
    public bool isUnlocked = false; // Status apakah padlock sudah terbuka

    void Update()
    {
        // Jika padlock sudah terbuka dan tombol Enter ditekan
        if (isUnlocked && Input.GetKeyDown(KeyCode.Return)) 
        {
            OpenChest();
        }
    }

    // Fungsi untuk membuka padlock ketika ruller berada di angka yang benar
    public void UnlockPadlock()
    {
        isUnlocked = true; // Menandakan bahwa padlock sudah terbuka
        Debug.Log("Padlock unlocked!");
    }

    void OpenChest()
    {
        // Menyembunyikan peti tertutup dan menampilkan peti terbuka
        chestClosed.SetActive(false);
        chestOpen.SetActive(true);
        RustKey.SetActive(true);

        // Menonaktifkan padlock setelah peti terbuka
        padlock.SetActive(false);

        Debug.Log("Chest opened!");
    }
}

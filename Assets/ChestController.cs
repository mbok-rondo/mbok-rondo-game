using UnityEngine;

public class ChestController : MonoBehaviour
{
    public GameObject chestClosed;  // Referensi ke objek ChestClosed
    public GameObject chestOpen;    // Referensi ke objek ChestOpen
    public GameObject padlock;
    public GameObject RustKey;       // Referensi ke padlock

    void Update()
    {
        // Ketika tombol Enter ditekan
        if (Input.GetKeyDown(KeyCode.Return))  // KeyCode.Return untuk Enter
        {
            OpenChest();
        }
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

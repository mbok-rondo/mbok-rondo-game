using UnityEngine;

public class PadlockController : MonoBehaviour
{
    public GameObject chestClosed;  // Referensi ke objek ChestClosed
    public GameObject chestOpen;    // Referensi ke objek ChestOpen
    public GameObject padlock;
    public GameObject RustKey;      // Referensi ke padlock (yang mengontrol peti terbuka)
    public bool isUnlocked = false; // Status apakah padlock sudah terbuka
    public string correctCode;      // Kode yang benar

    // Daftar ruller yang akan digunakan untuk membentuk kode
    public RullerController[] rullers; 
    public Camera cameraMain;
    public Camera cameraPuzzle;

    void Update()
    {
        // Jika tombol Enter ditekan, periksa apakah kode benar
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckCode();  // Periksa kode yang dimasukkan
        }
    }

    // Fungsi untuk memeriksa apakah kode yang dimasukkan benar
    void CheckCode()
    {
        // Menggabungkan nilai-nilai dari setiap ruller menjadi satu string kode
        string enteredCode = "";
        foreach (RullerController ruller in rullers)
        {
            enteredCode += ruller.currentValue.ToString();  // Ambil nilai dari setiap ruller
        }

        // Tampilkan kode yang dimasukkan di Debug Log
        Debug.Log("Kode yang dimasukkan: " + enteredCode);  // Menampilkan kode yang dimasukkan di console debug

        // Periksa apakah kode yang dimasukkan benar
        if (enteredCode == correctCode)
        {
            Unlock();  // Jika kode benar, buka padlock
        }
        else
        {
            WrongCode();  // Jika kode salah, beri tahu pemain
        }
    }

    // Fungsi untuk membuka padlock jika kode benar
    public void Unlock()
    {
        isUnlocked = true;  // Set isUnlocked menjadi true jika kode benar
        Debug.Log("Kode benar! Padlock dibuka.");

        // Tampilkan peti terbuka dan sembunyikan peti tertutup
        chestClosed.SetActive(false);
        chestOpen.SetActive(true);
        RustKey.SetActive(true);

        // Nonaktifkan padlock
        padlock.SetActive(false);
        // Kamera kembali ke kamera utama
        if (cameraMain != null && cameraPuzzle != null)
        {
            cameraMain.enabled = true;
            cameraPuzzle.enabled = false;
        }
    }

    // Fungsi jika kode salah
    public void WrongCode()
    {
        Debug.Log("Kode salah! Coba lagi.");
    }
}

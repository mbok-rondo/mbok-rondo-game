using UnityEngine;

public class GantiTextMesh : MonoBehaviour
{
    public TextMesh targetTextMesh;                // Kertas tempat menampilkan angka
    public int digit = 4;                          // Jumlah digit kode
    public PadlockController padlockController;    // Referensi ke skrip PadlockController
    public ChestController chestController;        // Referensi ke ChestController

    void Start()
    {
        // Generate kode baru
        string kodeBaru = GenerateRandomNumber(digit);

        // Tampilkan kode di kertas
        if (targetTextMesh != null)
            targetTextMesh.text = kodeBaru;

        // Kirim ke PadlockController
        if (padlockController != null)
            padlockController.correctCode = kodeBaru;

        Debug.Log("Kode yang benar: " + kodeBaru);
    }

    string GenerateRandomNumber(int length)
    {
        string result = "";
        for (int i = 0; i < length; i++)
        {
            result += Random.Range(0, 10).ToString();
        }
        return result;
    }

    void Update()
    {
        // Periksa ketika tombol Enter ditekan
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // Misalnya kode yang dimasukkan dari RullerController atau variabel lain
            string enteredCode = GetInputCode();  // Ambil kode yang dimasukkan pemain (misal dari RullerController)

            // Cek apakah kode yang dimasukkan benar
            if (enteredCode == padlockController.correctCode)
            {
                padlockController.Unlock(); // Buka padlock
                chestController.OpenChest(); // Panggil OpenChest() untuk membuka peti
            }
            else
            {
                padlockController.WrongCode(); // Kode salah
            }
        }
    }

    // Fungsi untuk mendapatkan kode yang dimasukkan pemain
    string GetInputCode()
    {
        // Di sini Anda bisa mengambil kode yang dimasukkan dari RullerController atau sistem lain
        // Misalnya Anda dapat menggabungkan nilai dari RullerController untuk mendapatkan kode
        return "1234";  // Ganti dengan logika untuk mendapatkan kode yang benar dari input RullerController
    }
}

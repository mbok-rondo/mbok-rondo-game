using UnityEngine;

public class GantiTextMesh : MonoBehaviour
{
    public TextMesh targetTextMesh;                // Kertas tempat menampilkan angka
    public int digit = 4;                          // Jumlah digit kode
    public PadlockController padlockController;    // Referensi ke skrip PadlockController
    public ChestController chestController;        // Referensi ke ChestController
    public RullerController[] rullers;             // Referensi ke ruller milik chest ini

    void Start()
    {
        // Generate kode acak
        string kodeBaru = GenerateRandomNumber(digit);

        // Tampilkan di kertas
        if (targetTextMesh != null)
            targetTextMesh.text = kodeBaru;

        // Kirim ke padlock
        if (padlockController != null)
            padlockController.correctCode = kodeBaru;

        Debug.Log("Kode yang benar (untuk " + gameObject.name + "): " + kodeBaru);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            string enteredCode = GetInputCode();

            if (enteredCode == padlockController.correctCode)
            {
                padlockController.Unlock();
                chestController.OpenChest();
            }
            else
            {
                padlockController.WrongCode();
            }
        }
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

    // Ambil nilai dari semua ruller yang terhubung
    string GetInputCode()
    {
        string result = "";
        foreach (RullerController r in rullers)
        {
            result += r.currentValue.ToString();
        }
        Debug.Log("Kode dimasukkan: " + result);
        return result;
    }
}

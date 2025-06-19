using UnityEngine;
using UnityEngine.UI;

public class GantiTextMesh : MonoBehaviour
{
    public TextMesh targetTextMesh;                // Kertas tempat menampilkan angka
    public int digit = 4;                          // Jumlah digit kode
    public PadlockController padlockController;    // Referensi ke skrip PadlockController
    public ChestController chestController;        // Referensi ke ChestController
    public RullerController[] rullers;             // Referensi ke ruller milik chest ini
    public string GeneratedCode { get; private set; }

    // Tambahan UI
    public Text textKode1;
    public Text textKode2;
    public Text textKode3;
    public Text textKode4;
    private int uiSlotIndex = -1; // Menyimpan index UI slot

    void Start()
    {
        // Generate kode acak
        GeneratedCode = GenerateRandomNumber(digit);

        // Tampilkan di kertas
        if (targetTextMesh != null)
            targetTextMesh.text = GeneratedCode;

        // Kirim ke padlock
        if (padlockController != null)
            padlockController.correctCode = GeneratedCode;

        Debug.Log("Kode yang benar (untuk " + gameObject.name + "): " + GeneratedCode);
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

                SembunyikanKodeUI(); // ⬅️ Sembunyikan text UI saat chest dibuka
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

    // Dipanggil dari PickKertas.cs
    public void TampilkanKodeDiSlot(string kode, int slotIndex)
    {
        uiSlotIndex = slotIndex;

        switch (slotIndex)
        {
            case 1:
                if (textKode1 != null)
                {
                    textKode1.text = "Kode Kertas 1: " + kode;
                    textKode1.gameObject.SetActive(true);
                }
                break;
            case 2:
                if (textKode2 != null)
                {
                    textKode2.text = "Kode Kertas 2: " + kode;
                    textKode2.gameObject.SetActive(true);
                }
                break;
            case 3:
                if (textKode3 != null)
                {
                    textKode3.text = "Kode Kertas 3: " + kode;
                    textKode3.gameObject.SetActive(true);
                }
                break;
            case 4:
                if (textKode4 != null)
                {
                    textKode4.text = "Kode Kertas 4: " + kode;
                    textKode4.gameObject.SetActive(true);
                }
                break;
            default:
                Debug.LogWarning("Slot UI tidak valid");
                break;
        }
    }

    // Sembunyikan text saat chest terbuka
    void SembunyikanKodeUI()
    {
        switch (uiSlotIndex)
        {
            case 1:
                if (textKode1 != null) textKode1.gameObject.SetActive(false);
                break;
            case 2:
                if (textKode2 != null) textKode2.gameObject.SetActive(false);
                break;
            case 3:
                if (textKode3 != null) textKode3.gameObject.SetActive(false);
                break;
            case 4:
                if (textKode4 != null) textKode4.gameObject.SetActive(false);
                break;
        }
    }
}

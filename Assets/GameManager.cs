using UnityEngine;
using TMPro; // Jangan lupa untuk TextMeshPro

public class GameManager : MonoBehaviour
{
    // Singleton pattern agar mudah diakses dari mana saja
    public static GameManager instance;

    [Header("Game Elements")]
    public GameObject finishArea; // Area finish yang akan muncul
    public int totalKeys = 4;     // Jumlah kunci untuk menang

    [Header("UI Elements")]
    public TextMeshProUGUI keyCountText; // Teks untuk jumlah kunci
    public GameObject missionCompleteUI;  // UI untuk layar kemenangan
    public GameObject defeatUI;           // UI untuk layar kekalahan

    // Variabel internal untuk melacak progres
    private int keysCollected = 0;

    private void Awake()
    {
        // Setup Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Mengatur kondisi awal permainan
        keysCollected = 0;
        Time.timeScale = 1f; // Memastikan waktu game berjalan normal

        // Memastikan semua UI kondisi akhir tidak aktif di awal
        if (finishArea != null) finishArea.SetActive(false);
        if (missionCompleteUI != null) missionCompleteUI.SetActive(false);
        if (defeatUI != null) defeatUI.SetActive(false);

        // Update UI jumlah kunci di awal
        UpdateKeyUI();
    }

    // --- FUNGSI UNTUK MEKANIK KUNCI & KEMENANGAN ---

    // Dipanggil oleh skrip Key.cs
    public void CollectKey()
    {
        keysCollected++;
        UpdateKeyUI();

        // Cek apakah kondisi menang sudah terpenuhi
        if (keysCollected >= totalKeys)
        {
            ActivateFinishArea();
        }
    }

    void UpdateKeyUI()
    {
        if (keyCountText != null)
        {
            keyCountText.text = $"Kunci: {keysCollected} / {totalKeys}";
        }
    }

    void ActivateFinishArea()
    {
        if (finishArea != null)
        {
            finishArea.SetActive(true);
        }
    }

    // Dipanggil oleh skrip FinishArea.cs
    public void CompleteMission()
    {
        if (missionCompleteUI != null)
        {
            missionCompleteUI.SetActive(true);
            Time.timeScale = 0f; // Menghentikan permainan
        }
    }

    // --- FUNGSI UNTUK KONDISI KALAH ---

    // Dipanggil oleh skrip EnemyLogic.cs
    public void TriggerDefeat()
    {
        if (defeatUI != null)
        {
            defeatUI.SetActive(true);
            Time.timeScale = 0f; // Menghentikan permainan
        }
    }
}
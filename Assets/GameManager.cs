using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Dibutuhkan untuk load scene

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Elements")]
    public GameObject finishArea;
    public int totalKeys = 4;
    [Header("Gameplay Characters")]
public GameObject player;
public GameObject[] enemies;
    [Header("UI Elements")]
    public TextMeshProUGUI keyCountText;
    public GameObject missionCompleteUI;
    public GameObject defeatUI;
[Header("UI Notifikasi")]
public TextMeshProUGUI notificationText;

    private int keysCollected = 0;

    private void Awake()
    {
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
        keysCollected = 0;
        Time.timeScale = 1f;

        if (finishArea != null) finishArea.SetActive(false);
        if (missionCompleteUI != null) missionCompleteUI.SetActive(false);
        if (defeatUI != null) defeatUI.SetActive(false);

        UpdateKeyUI();
    }

    public void CollectKey()
    {
        keysCollected++;
        UpdateKeyUI();

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
         ShowNotification("Finish area telah terbuka!", Color.green);
    }

    public void CompleteMission()
{
    if (missionCompleteUI != null)
    {
        missionCompleteUI.SetActive(true);
        Time.timeScale = 0f;

        // Matikan player & enemy agar tidak ganggu UI
        if (player != null) player.SetActive(false);
        foreach (var enemy in enemies)
        {
            if (enemy != null) enemy.SetActive(false);
        }
    }
}

    public void TriggerDefeat()
    {
        if (defeatUI != null)
        {
            defeatUI.SetActive(true);
            Time.timeScale = 0f;
        }
        // Matikan player & enemy agar tidak ganggu UI
        if (player != null) player.SetActive(false);
        foreach (var enemy in enemies)
        {
            if (enemy != null) enemy.SetActive(false);
        }
    }
public void ShowNotification(string message, Color color)
{
    if (notificationText != null)
    {
        notificationText.text = message;
        notificationText.color = color;
    }
}
public int GetKeyCount()
{
    return keysCollected;
}

    // ---------------------------------------------
    // 🆕 FUNGSI UNTUK BUTTON
    // ---------------------------------------------

    public void TryAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Fungsi Back to Main Menu
    public void BackToMainMenu()
    {
        Time.timeScale = 1f; // Resume game time
        SceneManager.LoadScene("UI_Play"); // Ganti dengan nama scene Main Menu kamu
    }
}

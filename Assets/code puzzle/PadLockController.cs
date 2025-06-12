using UnityEngine;

public class PadlockController : MonoBehaviour
{
    public GameObject chestClosed;
    public GameObject chestOpen;
    public GameObject padlock;
    public GameObject RustKey;
    public bool isUnlocked = false;
    public string correctCode;
    public static bool isPuzzleActive = false;

    public RullerController[] rullers;
    public Camera cameraMain;
    public Camera cameraPuzzle;
    public PlayerLogic player;

   void Update()
{
    if (Input.GetKeyDown(KeyCode.Return))
    {
        CheckCode();
    }

    // ✅ Keluar dari mode puzzle dengan tombol Escape
    if (isPuzzleActive && Input.GetKeyDown(KeyCode.Escape))
    {
        ExitPuzzleMode();
    }
}

    void CheckCode()
    {
        string enteredCode = "";
        foreach (RullerController ruller in rullers)
        {
            enteredCode += ruller.currentValue.ToString();
        }

        Debug.Log("Kode yang dimasukkan: " + enteredCode);

        if (enteredCode == correctCode)
        {
            Unlock();
        }
        else
        {
            WrongCode();
        }
    }

    public void Unlock()
    {
        isUnlocked = true;
        Debug.Log("Kode benar! Padlock dibuka.");

        chestClosed.SetActive(false);
        chestOpen.SetActive(true);
        RustKey.SetActive(true);
        padlock.SetActive(false);

        ExitPuzzleMode(); // Keluar dari mode puzzle
    }

    private void OnMouseDown()
    {
        if (!isUnlocked)
        {
            EnterPuzzleMode(); // ✅ Ubah ke nama fungsi yang benar
        }
    }

    void EnterPuzzleMode()
{
    if (cameraMain != null && cameraPuzzle != null)
    {
        cameraMain.enabled = false;
        cameraPuzzle.enabled = true;
    }

    if (player != null)
    {
        player.canMove = false;
    }

    // ⬇️ Tambahkan baris ini di sini
    RullerController.allRullers = this.rullers;

    PadlockController.isPuzzleActive = true;

    if (rullers.Length > 0)
    {
        RullerController.selectedIndex = 0;

        // Panggil fungsi untuk memilih ruller pertama
        typeof(RullerController)
            .GetMethod("SelectRullerByIndex", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
            ?.Invoke(null, new object[] { 0 });
    }

    Debug.Log("Masuk mode puzzle: " + gameObject.name);
}

    void ExitPuzzleMode()
    {
        if (cameraMain != null && cameraPuzzle != null)
        {
            cameraMain.enabled = true;
            cameraPuzzle.enabled = false;
        }

        if (player != null)
        {
            player.canMove = true;
        }

        isPuzzleActive = false;
        Debug.Log("Keluar dari mode puzzle.");
    }

    public void WrongCode()
    {
        Debug.Log("Kode salah! Coba lagi.");
    }
}

using UnityEngine;

public class RullerController : MonoBehaviour
{
    public int currentValue = 0;
    public int maxValue = 9;
private AudioSource audioSource;
   public static int selectedIndex = 0;
public static RullerController[] allRullers;
    private static RullerController selectedRuller;
    

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // // Hanya inisialisasi sekali
        // if (allRullers == null || allRullers.Length == 0)
        // {
        //     allRullers = FindObjectsOfType<RullerController>();
        // }
    }
public static void SelectFirstRuller()
{
    if (allRullers != null && allRullers.Length > 0)
    {
        selectedIndex = 0;
        SelectRullerByIndex(selectedIndex);
    }
}
    private void OnMouseDown()
{
    for (int i = 0; i < allRullers.Length; i++)
    {
        if (allRullers[i] == this)
        {
            SelectRullerByIndex(i);
            Debug.Log("Selected by click: Ruller" + (i + 1));
            break;
        }
    }
}

   void Update()
{
    if (!PadlockController.isPuzzleActive) return;

    // Pemilihan Ruller langsung dengan tombol angka 1-4
    if (Input.GetKeyDown(KeyCode.Alpha1))
    {
        SelectRullerByIndex(0);
        Debug.Log("Masuk ke Ruller 1");
    }
    else if (Input.GetKeyDown(KeyCode.Alpha2))
    {
        SelectRullerByIndex(1);
        Debug.Log("Masuk ke Ruller 2");
    }
    else if (Input.GetKeyDown(KeyCode.Alpha3))
    {
        SelectRullerByIndex(2);
        Debug.Log("Masuk ke Ruller 3");
    }
    else if (Input.GetKeyDown(KeyCode.Alpha4))
    {
        SelectRullerByIndex(3);
        Debug.Log("Masuk ke Ruller 4");
    }

    // Navigasi dan rotasi untuk ruller yang sedang aktif
    if (selectedRuller == this)
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            RotateUp();
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            RotateDown();
        }
    }
}



  void RotateUp()
{
    currentValue = (currentValue + 1) % (maxValue + 1);
    transform.Rotate(-36f, 0, 0);
    Debug.Log(name + " value: " + currentValue);
    PlaySound();
}

void RotateDown()
{
    currentValue = (currentValue - 1 + (maxValue + 1)) % (maxValue + 1);
    transform.Rotate(36f, 0, 0);
    Debug.Log(name + " value: " + currentValue);
    PlaySound();
}

void PlaySound()
{
    if (audioSource != null && audioSource.clip != null)
    {
        audioSource.Play();
    }
}

    void SelectThisRuller()
    {
        for (int i = 0; i < allRullers.Length; i++)
        {
            if (allRullers[i] == this)
            {
                selectedIndex = i;
                break;
            }
        }
        selectedRuller = this;
        Debug.Log("Selected by click: " + name);
    }

 public static void SelectRullerByIndex(int index)
{
    if (index >= 0 && index < allRullers.Length)
    {
        selectedIndex = index;
        selectedRuller = allRullers[index];
        Debug.Log("Selected by key: " + selectedRuller.name); // ✅ Benar
    }
}
}

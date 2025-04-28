using UnityEngine;

public class RullerController : MonoBehaviour
{
    public int currentValue = 0;
    public int maxValue = 9;

    public static RullerController[] allRullers; // array ruller
    private static int selectedIndex = 0;        // indeks ruller aktif
    private static RullerController selectedRuller;

    void Start()
    {
        // Hanya inisialisasi sekali
        if (allRullers == null || allRullers.Length == 0)
        {
            allRullers = FindObjectsOfType<RullerController>();
        }
    }

    private void OnMouseDown()
    {
        SelectThisRuller();
    }

    void Update()
    {
        // Hanya ruller aktif yang bisa diputar
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

  // Navigasi kanan/kiri
if (Input.GetKeyDown(KeyCode.LeftArrow))
{
    selectedIndex = (selectedIndex + 1  + allRullers.Length) % allRullers.Length - selectedIndex ;

    SelectRullerByIndex(selectedIndex);
}
else if (Input.GetKeyDown(KeyCode.RightArrow))
{
    selectedIndex = (selectedIndex - 1 + allRullers.Length) % allRullers.Length;
    SelectRullerByIndex(selectedIndex);
}
        }
    }

    void RotateUp()
    {
        currentValue = (currentValue + 1) % (maxValue + 1);
        transform.Rotate(-36f, 0, 0);
        Debug.Log(name + " value: " + currentValue);
    }

    void RotateDown()
    {
        currentValue = (currentValue - 1 + (maxValue + 1)) % (maxValue + 1);
        transform.Rotate(36f, 0, 0);
        Debug.Log(name + " value: " + currentValue);
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

    static void SelectRullerByIndex(int index)
{
    selectedRuller = allRullers[index];
    Debug.Log("Selected by arrow key: " + selectedRuller.name); // Perbaikan typo di sini
}

}

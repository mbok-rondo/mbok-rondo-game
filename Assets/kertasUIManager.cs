using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KertasUIManager : MonoBehaviour
{
    public static KertasUIManager Instance;

    public GameObject kodePrefab; // UI Text prefab
    public Transform parentPanel; // Panel di pojok kiri bawah untuk menampung kode
    private Dictionary<PadlockController, GameObject> kodeMap = new Dictionary<PadlockController, GameObject>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void TampilkanKode(string kode, PadlockController padlock)
    {
        GameObject kodeObj = Instantiate(kodePrefab, parentPanel);
        kodeObj.GetComponent<Text>().text = kode;

        kodeMap[padlock] = kodeObj;
    }

    public void HapusKode(PadlockController padlock)
    {
        if (kodeMap.ContainsKey(padlock))
        {
            Destroy(kodeMap[padlock]);
            kodeMap.Remove(padlock);
        }
    }
}

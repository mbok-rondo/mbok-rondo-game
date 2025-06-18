using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class KertasUIManager : MonoBehaviour
{
    public static KertasUIManager Instance;
    public Text kodeTextUI; // Assign ke Text kiri bawah di Canvas

    private List<string> semuaKode = new List<string>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void TambahkanKode(string kodeBaru)
    {
        if (!semuaKode.Contains(kodeBaru))
        {
            semuaKode.Add(kodeBaru);
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        kodeTextUI.text = "Kode yang ditemukan:\n";
        foreach (string kode in semuaKode)
        {
            kodeTextUI.text += "- " + kode + "\n";
        }
    }
}

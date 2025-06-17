using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KodeUIManager : MonoBehaviour
{
    public static KodeUIManager Instance;

    public GameObject kodeTextTemplate; // Text UI prefab
    public RectTransform canvasTransform; // untuk posisi parent
    private List<GameObject> kodeTexts = new List<GameObject>();

    private float startY = 20f;     // posisi awal Y
    private float offsetY = 30f;    // jarak antar kode

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void TambahKode(string kode)
    {
        GameObject newText = Instantiate(kodeTextTemplate, canvasTransform);
        newText.SetActive(true);
        newText.GetComponent<Text>().text = kode;

        // Atur posisi secara manual
        RectTransform rt = newText.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(20f, -(startY + offsetY * kodeTexts.Count));

        kodeTexts.Add(newText);
    }

    public void HapusKode(string kode)
    {
        for (int i = 0; i < kodeTexts.Count; i++)
        {
            Text t = kodeTexts[i].GetComponent<Text>();
            if (t.text == kode)
            {
                Destroy(kodeTexts[i]);
                kodeTexts.RemoveAt(i);
                RepositionUI(); // refresh posisi
                break;
            }
        }
    }

    private void RepositionUI()
    {
        for (int i = 0; i < kodeTexts.Count; i++)
        {
            RectTransform rt = kodeTexts[i].GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(20f, -(startY + offsetY * i));
        }
    }
}

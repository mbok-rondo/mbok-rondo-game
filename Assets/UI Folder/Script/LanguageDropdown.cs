using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LanguageDropdown : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    void Start()
    {
        dropdown.onValueChanged.AddListener(OnLanguageChanged);
    }

    void OnLanguageChanged(int index)
    {
        if (LanguageManager.Instance == null)
        {
            Debug.LogWarning("LanguageManager belum tersedia.");
            return;
        }

        if (index == 0)
            LanguageManager.Instance.SetLanguage("en"); 
            LanguageManager.Instance.SetLanguage("id");
    }
}

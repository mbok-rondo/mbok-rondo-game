using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using System.Collections;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance;

    private void Awake()
    {
        // Singleton sederhana
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetLanguage(string languageCode)
    {
        // Jalankan coroutine lewat GameObject yang aktif
        PlayerPrefs.SetString("SelectedLanguage", languageCode);
        PlayerPrefs.Save();

        if (Instance != null && Instance.isActiveAndEnabled)
        {
            Instance.StartCoroutine(SetLocale(languageCode));
        }
        else
        {
            Debug.LogWarning("LanguageManager tidak aktif, menunggu hingga aktif...");
        }
    }

    private IEnumerator SetLocale(string code)
    {
        yield return LocalizationSettings.InitializationOperation;

        foreach (var locale in LocalizationSettings.AvailableLocales.Locales)
        {
            if (locale.Identifier.Code == code)
            {
                LocalizationSettings.SelectedLocale = locale;
                Debug.Log("Bahasa diubah ke: " + code);
                yield break;
            }
        }

        Debug.LogWarning("Bahasa tidak ditemukan: " + code);
    }
}

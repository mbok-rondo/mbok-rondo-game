using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
 public Slider slider;

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("volume", 0.5f);
        slider.value = savedVolume;
        SetVolume(savedVolume);

        slider.onValueChanged.AddListener((v) => {
            SetVolume(v);
            PlayerPrefs.SetFloat("volume", v);
        });
    }

    void SetVolume(float volume)
    {
        if (MusicManager.Instance != null)
            MusicManager.Instance.SetVolume(volume);
    }
}

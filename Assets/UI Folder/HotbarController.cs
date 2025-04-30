using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HotbarController : MonoBehaviour
{
    // Start is called before the first frame update
    public Image[] slots; // Drag semua slot di Inspector
    private int currentIndex = 0;
    void Start()
    {
        HighlightSlot(currentIndex);
    }

    // Update is called once per frame
    void Update()
    {
         for (int i = 0; i < slots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                currentIndex = i;
                HighlightSlot(currentIndex);
            }
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            currentIndex = (currentIndex + 1) % slots.Length;
            HighlightSlot(currentIndex);
        }
        else if (scroll < 0f)
        {
            currentIndex = (currentIndex - 1 + slots.Length) % slots.Length;
            HighlightSlot(currentIndex);
        }
    }

      void HighlightSlot(int index)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].color = (i == index) ? Color.yellow : Color.white;
        }

        // Debug: lihat slot aktif
        Debug.Log("Slot aktif: " + (index + 1));
    }
}
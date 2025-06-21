using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Cek apakah yang masuk adalah Player
        if (other.CompareTag("Players"))
        {
            // Panggil fungsi untuk menyelesaikan misi di GameManager
            GameManager.instance.CompleteMission();

            // Nonaktifkan area ini agar tidak memicu berulang kali
            gameObject.SetActive(false);
        }
    }
}

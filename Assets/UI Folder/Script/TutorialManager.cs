using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] tutorialPages; // Isi dengan panel tutorial
    public Button nextButton;
    public Button backButton;
    public Button finishButton;

    public GameObject tutorialPanel;

    // public MonoBehaviour[] playerScripts; // isi dengan script yang ingin dinonaktifkan
    // public MonoBehaviour[] enemyScripts;

    public GameObject player;
    public GameObject[] enemies;

    private int currentPage = 0;

    // Start is called before the first frame update
    void Start()
    {

        if (tutorialPages == null || tutorialPages.Length == 0)
    {
        Debug.LogError("tutorialPages belum diisi di Inspector!");
        return;
    }
    
        ShowPage(0);

        tutorialPanel.SetActive(true);

        //Untuk pake Script
        // foreach (var script in playerScripts)
        //     script.enabled = false;

        // foreach (var script in enemyScripts)
        //     script.enabled = false;

        //GameObject
        if (player != null)
            player.SetActive(false);

        foreach (var enemy in enemies)
            enemy.SetActive(false);
    }

    public void ShowPage(int index)
    {
        // Tampilkan hanya halaman saat ini
        for (int i = 0; i < tutorialPages.Length; i++)
        {
            tutorialPages[i].SetActive(i == index);
        }

        currentPage = index;

        // Atur tombol sesuai halaman
        backButton.gameObject.SetActive(index > 0);
        nextButton.gameObject.SetActive(index < tutorialPages.Length - 1);
        finishButton.gameObject.SetActive(index == tutorialPages.Length - 1);
    }

    public void NextPage()
    {
        if (currentPage < tutorialPages.Length - 1)
        {
            ShowPage(currentPage + 1);
        }
    }

    public void BackPage()
    {
        if (currentPage > 0)
        {
            ShowPage(currentPage - 1);
        }
    }

    public void FinishTutorial()
    {
        tutorialPanel.SetActive(false);

        // foreach (var script in playerScripts)
        //     script.enabled = true;

        // foreach (var script in enemyScripts)
        //     script.enabled = true;

        if (player != null)
            player.SetActive(true);

        foreach (var enemy in enemies)
            enemy.SetActive(true);

        Debug.Log("Tutorial selesai, game dimulai!");
    }
}

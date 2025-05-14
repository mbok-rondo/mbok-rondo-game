using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject gameplayUI;
    public GameObject pauseUI;
    // public GameObject optionUI;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused && pauseUI.activeSelf)
                Resume();
            else if (!isPaused)
                Pause();
        }
    }

    public void Resume()
    {
        gameplayUI.SetActive(true);
        pauseUI.SetActive(false);
        // optionUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Pause()
    {
        gameplayUI.SetActive(false);
        pauseUI.SetActive(true);
        // optionUI.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // public void OpenOption()
    // {
    //     pauseUI.SetActive(false);
    //     optionUI.SetActive(true);
    // }

    // public void BackFromOption()
    // {
    //     optionUI.SetActive(false);
    //     pauseUI.SetActive(true);
    // }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("UI_Play");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

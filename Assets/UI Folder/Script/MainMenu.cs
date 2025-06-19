using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Load Scene
    public void Play()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        SceneManager.LoadScene("Environtment");
    }

    //Quit Game
    public void Quit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
        Debug.Log("The Player has Quit the game");
    }
}
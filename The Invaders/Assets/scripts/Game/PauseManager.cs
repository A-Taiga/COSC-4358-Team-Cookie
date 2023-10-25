using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GUIController uiController;
    public static bool isPaused;
    void Start()
    {
        isPaused = false;
    }
    public void PauseGame()
    {
        if (isPaused)
            return; 

        Time.timeScale = 0f;
        if (uiController)
        {
            uiController.SetPauseScreen(true);
        }
        isPaused = true;
    }
    public void ResumeGame()
    {
        if (!isPaused)
            return;

        Time.timeScale = 1f;
        if (uiController)
        {
            uiController.SetPauseScreen(false);
        }
        isPaused = false;
    }
    public void RestartLevel()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ShowSettings()
    {
        if (uiController)
        {
            //uiController.ShowSettings(true);
            Debug.Log("No settings yet.");
        }
    }
    public void QuitToMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Debug.Log("Escape pressed.");
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

}

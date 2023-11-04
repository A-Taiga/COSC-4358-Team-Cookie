using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GUIController uiController;
    public GameObject MainPause;
    public GameObject Settings;
    public static bool isPaused;
    
    void Start()
    {
        Reset();
    }
    void Reset()
    {
        isPaused = false;
        MainPause?.SetActive(false);
        Settings?.SetActive(false);
    }
    public void PauseGame()
    {
        if (isPaused)
            return; 

        Time.timeScale = 0f;
        if (uiController)
        {
            uiController.SetPauseScreen(true);
            MainPause?.SetActive(true);
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
        Reset();
    }
    public void RestartLevel()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ShowSettings()
    {
        MainPause.SetActive(false);
        Settings?.SetActive(true);
    }
    public void CloseSettings()
    {
        MainPause?.SetActive(true);
        Settings?.SetActive(false);
    }
    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
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

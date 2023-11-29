using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{
    public string gameStartScene;
    public void StartGame()
    {
        FileManager.WriteToFile(SaveManager.SAVE_FILE, string.Empty);
        SaveManager.Instance = new SaveManager(SaveManager.SAVE_FILE);
        SceneManager.LoadScene(gameStartScene);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

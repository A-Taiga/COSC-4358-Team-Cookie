using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{
    public string gameStartScene;
    public void StartGame()
    {
        SceneLoadingManager.LoadScene("Cutscenes");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

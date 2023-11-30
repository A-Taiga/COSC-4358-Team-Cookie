using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueGame : MonoBehaviour
{
    
    void Awake()
    {
        string result;
        if (!FileManager.LoadFromFile(SaveManager.SAVE_FILE, out result) || string.IsNullOrEmpty(result))
        {
            this.gameObject.SetActive(false);
        }
    }
    
    public void StartGame()
    {
        SaveManager.Instance = new SaveManager(SaveManager.SAVE_FILE);
        var sceneName = SaveManager.Instance.LastActiveScene();
        if (string.IsNullOrEmpty(sceneName))
        {
            this.gameObject.SetActive(false);
            Debug.LogWarning("Could not load this save file.");
            return;
        }
        SceneLoadingManager.LoadScene(sceneName);
    }
}

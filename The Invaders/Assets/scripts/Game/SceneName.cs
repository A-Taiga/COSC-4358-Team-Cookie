using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneName : MonoBehaviour
{
    private float timeRemaining = 3;
    public static string sceneName { get; private set; }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("hello new scene !");
        timeRemaining = 3f;
        GetComponentInChildren<TMP_Text>().text = scene.name;
        gameObject.SetActive(true);
    }

    void Awake()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            if(timeRemaining <= 0)
            {
                gameObject.SetActive(false);
            }
        } 
    }
}

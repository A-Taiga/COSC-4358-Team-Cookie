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


    public GameObject playerMapPos;

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Debug.Log("hello new scene !");
        print(scene.name);
        timeRemaining = 3f;
        GetComponentInChildren<TMP_Text>().text = scene.name;
        gameObject.SetActive(true);
        switch(scene.name)
        {
            case "forrest":         playerMapPos.transform.localPosition = new Vector3(332, -16, 0);    break;
            case "Village2":        playerMapPos.transform.localPosition = new Vector3(-65, 284, 0);    break;
            case "North Beach":     playerMapPos.transform.localPosition = new Vector3(142, 362, 0);    break;
            case "SunsetBay":       playerMapPos.transform.localPosition = new Vector3(-124, -162, 0);  break;
            case "Lighthouse":      playerMapPos.transform.localPosition = new Vector3(-376, -270, 0);  break;
            case "wetlands":        playerMapPos.transform.localPosition = new Vector3(-641, 382, 0);   break;
            case "volcano islands": playerMapPos.transform.localPosition = new Vector3(590, -326, 0);   break;
            case "CastleIsland":    playerMapPos.transform.localPosition = new Vector3(538, 429, 0);    break;
        }

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

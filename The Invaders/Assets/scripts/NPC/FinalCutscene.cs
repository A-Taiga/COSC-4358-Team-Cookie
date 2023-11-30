using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class FinalCutscene : MonoBehaviour
{
    public GameObject finalBoss;

    public GameObject csObject;
    private VideoPlayer cutScene;
    // Start is called before the first frame update
    void Start()
    {
        cutScene = csObject.GetComponent<VideoPlayer>();
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && finalBoss == null)
        {
            SceneManager.LoadScene("FinalCutscene");
        }
    }
    
}

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
    public SpriteRenderer door;

    public Sprite openDoor;
    // Start is called before the first frame update
    void Start()
    {
        cutScene = csObject.GetComponent<VideoPlayer>();
        StartCoroutine(BossDefeated());
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && finalBoss == null)
        {
            SceneManager.LoadScene("FinalCutscene");
        }
    }

    IEnumerator BossDefeated()
    {
        while (finalBoss != null)
        {
            yield return new WaitForSeconds(1f);
        }

        door.sprite = openDoor;
    }
    
}

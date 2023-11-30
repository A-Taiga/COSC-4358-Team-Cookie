using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideLevelChanger : MonoBehaviour
{
    public Collider2D coll;
    public string newLevelName;


    public void Start()
    {
        coll = GetComponent<Collider2D>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(!string.IsNullOrEmpty(newLevelName))
        {
            SceneLoadingManager.LoadScene(newLevelName);
        } 
    }
}

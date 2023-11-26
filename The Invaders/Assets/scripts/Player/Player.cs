using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static bool customSpawn = false;
    private static Vector3 spawnPos;

    private GameObject player;
    private GameObject cameraHolder;
    private HealthBar healthbar;
    public Quest currentQuest;

    
    // Game data, will fix persistence soon.
    public static int progress = 0;

    void Start()
    {
        player = getPlayerObject();
        healthbar = GetComponent<HealthBar>();
        
        if(customSpawn)
        {
            player.transform.position = spawnPos;
            CameraFollow.cameraHolder.transform.position = new Vector3(spawnPos.x, spawnPos.y, CameraFollow.cameraHolder.transform.position.z);
        }
        customSpawn = false;
        spawnPos = Vector3.zero;
        // DontDestroyOnLoad(gameObject);
        InventoryManager inventoryManager = GameObject.Find("Canvas").GetComponentInChildren<InventoryManager>();
        if(inventoryManager.hasSword == true)
        {
            player.GetComponent<Animator>().SetTrigger("PickedUpSword");
        }

    }

    public static void setCustomSpawn(Vector3 pos)
    {
        if (!customSpawn)
        {
            customSpawn = true;
            spawnPos = pos;
        }
    }

    public static GameObject getPlayerObject() {
        return GameObject.FindWithTag(TagManager.PLAYER_TAG);
    }

}
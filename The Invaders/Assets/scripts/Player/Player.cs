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
    public int gold; 


    void Start() {
        player = getPlayerObject();
        healthbar = GetComponent<HealthBar>();
        
        if(customSpawn)
        {
            player.transform.position = spawnPos;
            CameraFollow.cameraHolder.transform.position = new Vector3(spawnPos.x, spawnPos.y, CameraFollow.cameraHolder.transform.position.z);
        }
        customSpawn = false;
        spawnPos = Vector3.zero;
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject player;
    private HealthBar healthbar;
    public Quest currentQuest;
    public int gold; 


    void Awake() {
        player = getPlayerObject();
        healthbar = GetComponent<HealthBar>();
    }

    public static GameObject getPlayerObject() {
        return GameObject.FindWithTag(TagManager.PLAYER_TAG);
    }

}
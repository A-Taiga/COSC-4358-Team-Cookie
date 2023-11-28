using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
 
public class LevelMove_Ref : MonoBehaviour
{
    private GameObject playerObject;
    private Player player;
    
    public int sceneBuildIndex;
    public bool useName;
    public string sceneName;
    public bool teleportOnLoad = false;
    public Vector3 spawnPos;
    
    public int progressToEnter = 0;

    public void Start()
    {
        playerObject = Player.getPlayerObject();
        player = playerObject.GetComponent<Player>();
    }

    // Level move zoned enter, if collider is a player
    // Move game to another scene
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Could use other.GetComponent<Player>() to see if the game object has a Player component
        // Tags work too. Maybe some players have different script components?
        if(other.tag == TagManager.PLAYER_TAG) {
            
            if (player.progress < progressToEnter)
            {
                playerObject.GetComponentInChildren<PopupMessage>().ShowPopup("I don't think I should go here right now...", 2f);
                return;
            }
            
            // Player entered, so move level
            if (useName)
            {
                print("Switching Scene to " + sceneName);
                SceneTransition.instance.ChangeLevel(sceneName);
            }
            else
            {
                print("Switching Scene to " + sceneBuildIndex);
                SceneTransition.instance.ChangeLevel(sceneBuildIndex);
            }
            if (teleportOnLoad)
            {
                Player.setCustomSpawn(spawnPos);
            }
            SaveManager.Instance.CommitSave();
        }
    }
}
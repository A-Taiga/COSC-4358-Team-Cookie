using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class LevelMove_Ref : MonoBehaviour
{
    public int sceneBuildIndex;
    public bool useName;
    public string sceneName;

    public bool teleportOnLoad = false;
    public Vector3 spawnPos;


    // Level move zoned enter, if collider is a player
    // Move game to another scene
    private void OnTriggerEnter2D(Collider2D other) {
        print("Trigger Entered");
        
        // Could use other.GetComponent<Player>() to see if the game object has a Player component
        // Tags work too. Maybe some players have different script components?
        if(other.tag == "Player") {
            // Player entered, so move level
            if (useName)
            {
                print("Switching Scene to " + sceneName);
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                print("Switching Scene to " + sceneBuildIndex);
                SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
            }
            if (teleportOnLoad)
            {
                Player.setCustomSpawn(spawnPos);
            }
        }
    }
}
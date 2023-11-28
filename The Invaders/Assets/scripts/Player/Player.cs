using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour, ISaveable
{
    private static bool customSpawn = false;
    private static Vector3 spawnPos;

    private GameObject player;
    private GameObject cameraHolder;
    private HealthBar healthbar;
    public Quest currentQuest;

    public bool resetSaveOnAwake = false;
    
    // Game data, will fix persistence soon.
    public int progress = 0;

    void Awake()
    {
        if (resetSaveOnAwake)
        {
            FileManager.WriteToFile("savegame.json", "");
        }
    }

    void Start()
    {        
        SaveManager.Instance.LoadData(this);
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

    private void OnDestroy()
    {
        SaveManager.Instance.SaveData(this);
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

    public void PopulateSaveData(SaveData save)
    {
        save.playerData.playerProgress = progress;
        save.playerData.lastScene = SceneManager.GetActiveScene().name;
        save.playerData.spawnPos = player.transform.position;
    }

    public void LoadFromSaveData(SaveData save)
    {
        progress = save.playerData.playerProgress;
        if (SceneManager.GetActiveScene().name.Equals(save.playerData.lastScene))
        {
            Debug.Log("custom spawn set!");
            setCustomSpawn(save.playerData.spawnPos);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            SaveManager.Instance.SaveData(this);
            SaveManager.Instance.CommitSave();
        }
        if (Input.GetKeyDown(KeyCode.F9))
        {
            FileManager.WriteToFile("savegame.json", "");
        }
    }
}
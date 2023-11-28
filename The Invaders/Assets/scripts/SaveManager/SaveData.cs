using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{

    [System.Serializable]
    public struct PlayerData
    {
        public string lastScene;
        public Vector3 spawnPos;
        public int playerProgress;
        public int playerHealth;
        public int playerStamina;
    }

    [System.Serializable]
    public struct InventoryData {
        public string itemName;
        public int itemAmount;
        public int slotIndex;

    }
    [System.Serializable]
    public struct EnemyData
    {
        public string enemyName;
        public bool respawnEnemy;
        public float enemyHealth;
    }

    [System.Serializable]
    public struct DialogueData
    {
        public string dialogName;
        public bool dialogSeen;
    }

    public bool seenIntroCam;
    public PlayerData playerData;
    public List<InventoryData> m_InventoryData = new List<InventoryData>();
    public List<EnemyData> m_EnemyData = new List<EnemyData>();
    public List<DialogueData> m_DialogueData = new List<DialogueData>();

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string a_Json)
    {
        JsonUtility.FromJsonOverwrite(a_Json, this);
    }
}

public interface ISaveable
{
    void PopulateSaveData(SaveData a_SaveData);
    void LoadFromSaveData(SaveData a_SaveData);
}
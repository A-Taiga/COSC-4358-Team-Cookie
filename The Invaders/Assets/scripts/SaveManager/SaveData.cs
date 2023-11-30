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
        public float playerHealth;
        public float playerStamina;
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

    [System.Serializable]
    public struct WorldItem
    {
        public int wiHash;
        public bool wiPickedUp;
    }
    
    public bool seenIntroCam;
    public bool hasWetlandsKey;
    public bool hasSwordUpgrade;
    public bool hasVolcanoIslandsKey;
    public bool hasShield;
    public bool forestVillagerSaved;
    
    public int coinCount;
    
    public PlayerData playerData;
    public List<InventoryData> m_InventoryData = new List<InventoryData>();
    public List<EnemyData> m_EnemyData = new List<EnemyData>();
    public List<DialogueData> m_DialogueData = new List<DialogueData>();
    public List<WorldItem> m_WorldItems = new List<WorldItem>();
    
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
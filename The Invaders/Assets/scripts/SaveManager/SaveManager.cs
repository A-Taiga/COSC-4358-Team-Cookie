using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveManager
{
    public static readonly string SAVE_FILE = "savegame.json";
    public static SaveManager Instance = new SaveManager();
    private SaveData currentSave;
    public string saveName { get; private set; }
    
    public SaveManager() {
        currentSave = new SaveData();
        saveName = "tempsave.json";
    }
    public SaveManager(string saveName)
    {
        currentSave = new SaveData();
        this.saveName = saveName;
        ReloadSave();
    }

    public void ReloadSave()
    {
        if (FileManager.LoadFromFile(saveName, out var json))
        {
            currentSave.LoadFromJson(json);
        }        
        Debug.Log("Load successful");
    }

    public void CommitSave()
    {
        if (FileManager.WriteToFile(saveName, currentSave.ToJson()))
        {
            Debug.Log("Save successful");
        }
    }
    public void SaveData(ISaveable saveable)
    {
        saveable.PopulateSaveData(currentSave);
    }
    
    public void SaveData(IEnumerable<ISaveable> a_Saveables)
    {
        foreach (ISaveable saveable in a_Saveables)
        {
            saveable.PopulateSaveData(currentSave);
        }
        
    }
    
    public void LoadData(ISaveable saveable)
    {
        saveable.LoadFromSaveData(currentSave);
    }
    
    public void LoadData(IEnumerable<ISaveable> a_Saveables)
    {
        foreach (ISaveable saveable in a_Saveables)
        {
            saveable.LoadFromSaveData(currentSave);
        }
        
    }

    public string LastActiveScene()
    {
        return currentSave.playerData.lastScene;
    }

    public void ForceSave()
    {
        if (saveName.Equals(SaveManager.SAVE_FILE))
        {

            var savables = GameObject.FindObjectsOfType<MonoBehaviour>()
                .OfType<ISaveable>()
                .ToArray();

            SaveData(savables);
            CommitSave();
        }
    }

    ~SaveManager()
    {
        //CommitSave(); - for production use
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager
{
    public static SaveManager Instance = new SaveManager("savegame.json");
    private SaveData currentSave;
    private string saveName;
    
    SaveManager(string saveName)
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

    ~SaveManager()
    {
        //CommitSave(); - for production use
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour, ISaveable
{
    public Item[] items;
    public int maxStackedItems = 4;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    public ItemInfo itemInfo;

    public bool hasSword = false;

    int selectedSlot = -1;

    public int coinCount = 0;

    public GameObject coins;

    private bool showCheats = false;
    public GameObject cheats;
    
    public static InventoryManager Instance { private set; get; }
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(transform.root.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        var scene = SceneManager.GetActiveScene().name;
        if (scene.Equals("StartMenu"))
        {
            transform.root.gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        SaveManager.Instance.LoadData(this);
        Events<OnSceneChange>.Instance.Register(SceneChange);

        if (cheats)
        {
            showCheats = false;
            cheats.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        Events<OnSceneChange>.Instance.Unregister(SceneChange);
    }

    void SceneChange(string newScene)
    {
        SaveManager.Instance.SaveData(this);
    }
    
    public void ChangeSelectedSlot(int newValue)
    {
        if(selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }
        
        inventorySlots[newValue].Select();
        selectedSlot = newValue;
        itemInfo.DisplaySelected(inventorySlots[newValue].transform.GetChild(0).GetComponent<Image>(),
        inventorySlots[newValue].transform.GetChild(0).GetComponent<InventoryItem>().item.itemName);
    }
    public virtual bool AddItem(Item item)
    {

        if(item.itemName == "Coin")
        {
            coinCount++;
            updateCoinCount();
            return true;
        }
        /* search for slots with same item */
        for(int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot != null 
            && itemInSlot.item == item 
            && itemInSlot.count < maxStackedItems
            && itemInSlot.item.stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }
        /* search for empty item slot */
        for(int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
    }
    public virtual void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }
    
    public virtual void updateCoinCount()
    {
        coins.GetComponentInChildren<TMP_Text>().text = "x " + coinCount;
    }
    public void erase()
    {
        for(int i = 0; i < inventorySlots.Length; i++)
        {

            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if(itemInSlot != null )
            {
                Destroy(itemInSlot.gameObject);
            }
        }
    }

    public void replace(Item item, int index, int count, int coins)
    {
        // SpawnNewItem(items[0], inventorySlots[0]);
        GameObject newItemGo = Instantiate(inventoryItemPrefab, inventorySlots[index].transform);
        newItemGo.GetComponent<InventoryItem>().count = count;
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
        coinCount = coins;
        updateCoinCount();
        Debug.Log("COIN COUNT: " + coinCount);
        // AddItem(items[0]);
    }
    
    public void PopulateSaveData(SaveData save)
    {
        save.m_InventoryData.Clear();
        save.hasSwordUpgrade = hasSword;
        save.coinCount = coinCount;
        
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i] != null)
            {
                var slotItem = inventorySlots[i].GetComponentInChildren<InventoryItem>();
                if (slotItem != null)
                {
                    var item = new SaveData.InventoryData();
                    item.itemName = slotItem.item.itemName;
                    item.itemAmount = slotItem.count;
                    item.slotIndex = i;

                    save.m_InventoryData.Add(item);
                }
            }
        }
    }

    public void LoadFromSaveData(SaveData save)
    {
        hasSword = save.hasSwordUpgrade;

        foreach (var item in save.m_InventoryData.ToList())
        {
            InventorySlot slot = inventorySlots[item.slotIndex];
            var _Item = Array.Find(items, i => { return i.itemName == item.itemName; });
            SpawnNewItem(_Item, slot);
            InventoryItem slotItem = slot.GetComponentInChildren<InventoryItem>();
            slotItem.count = item.itemAmount;
            slotItem.RefreshCount();
            save.m_InventoryData.Remove(item);
        }

        coinCount = save.coinCount;
        updateCoinCount();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            if (showCheats)
            {
                cheats.SetActive(false);
            } else {
                cheats.SetActive(true);
            }

            showCheats = !showCheats;
        }
    }
}

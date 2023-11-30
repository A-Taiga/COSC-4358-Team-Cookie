using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : InventoryManager
{

    public Item[] sellingItems;
    public AudioClip sellSound;
    
    void Awake()
    {
    }
    void Start()
    {  
        InventoryManager playerManager = InventoryManager.Instance;
        for(int i = 0 ;i  < playerManager.inventorySlots.Length; i++)
        {
            InventorySlot slot = playerManager.inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot != null)
            {
                Debug.Log("ITEM IN SLOT: " + i + " " + itemInSlot.item.name);
                SpawnNewItem2(itemInSlot.item, inventorySlots[i], itemInSlot.count);
            }
        }
        coinCount = playerManager.coinCount;
        this.updateCoinCount();
    }
    // Update is called once per frame
    void Update()
    {

    }
    public override bool AddItem(Item item)
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
                this.SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
    }
    public override void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }


    public void SpawnNewItem2(Item item, InventorySlot slot, int count)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        newItemGo.GetComponent<InventoryItem>().count = count;
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    public void sellButton()
    {
        InventoryItem inventoryItem = inventorySlots[12].transform.GetChild(0).gameObject.GetComponent<InventoryItem>();
        Debug.Log("ITEM NAME: " + inventoryItem.item.name);
        if(inventoryItem.item.name == "Diamond")
        {
            this.AddItem(sellingItems[0]);
            this.AddItem(sellingItems[0]);
            this.AddItem(sellingItems[0]);
            this.AddItem(sellingItems[0]);
        }
        else if(inventoryItem.item.name == "Healthpotion")
        {
            this.AddItem(sellingItems[0]);
        }
        else if(inventoryItem.item.name == "SpeedPotion")
        {
            this.AddItem(sellingItems[0]);
            this.AddItem(sellingItems[0]);
        }
        else
        {
            return;
        }
        var src = InventoryManager.Instance.transform.root.gameObject.GetComponent<AudioSource>();
        if (src && sellSound)
        {
            src.clip = sellSound;
            src.Play();
        }
        
        inventorySlots[12].GetComponentInChildren<InventoryItem>().count--;
        inventorySlots[12].GetComponentInChildren<InventoryItem>().RefreshCount();
        if(inventorySlots[12].GetComponentInChildren<InventoryItem>().count == 0)
        {
            Destroy(inventorySlots[12].GetComponentInChildren<InventoryItem>().gameObject);
        }
    }
    public override void updateCoinCount()
    {
        coins.transform.GetChild(1).GetComponent<TMP_Text>().text = "x " + coinCount;
    }
}

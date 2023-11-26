using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : InventoryManager
{
    // Start is called before the first frame update
    public Item[] sellingItems;
    

    void Start()
    {
        InventoryManager playerManager = FindObjectOfType<InventoryManager>();
        for(int i = 0 ;i  < playerManager.inventorySlots.Length; i++)
        {
            InventorySlot slot = playerManager.inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot != null)
            {
                Debug.Log("ITEM IN SLOT: " + i + " " + itemInSlot.item.name);
                SpawnNewItem(itemInSlot.item, inventorySlots[i], itemInSlot.count);
            }
        }

        SpawnNewItem(sellingItems[1], inventorySlots[12], 3);
        SpawnNewItem(sellingItems[2], inventorySlots[13], 3);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnNewItem(Item item, InventorySlot slot, int count)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        newItemGo.GetComponent<InventoryItem>().count = count;
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

}

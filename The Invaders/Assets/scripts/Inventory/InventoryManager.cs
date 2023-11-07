using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int maxStackedItems = 4;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    public ItemInfo itemInfo;

    int selectedSlot = -1;
    
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        Debug.Log(inventorySlots);
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

    public bool AddItem(Item item)
    {
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
    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }
}

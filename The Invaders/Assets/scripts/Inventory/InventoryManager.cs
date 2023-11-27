using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
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
    
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {

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
        coins.transform.GetChild(1).GetComponent<TMP_Text>().text = "x " + coinCount;
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
        // AddItem(items[0]);
    }
}

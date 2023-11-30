using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;
    void Awake()
    {
        // inventoryManager = GameObject.Find("Canvas").GetComponentInChildren<InventoryManager>();
    }
    public void PickupItem(int id)
    {
        bool result = inventoryManager.AddItem(itemsToPickup[id]);
        if (result == true)
        {
            Debug.Log("ITEM ADDED");
        }
    }
}

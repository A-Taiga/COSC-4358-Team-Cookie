using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class exitScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject shopManager;
    public Item[] items;

    private bool hasExited = false;

    void Awake()
    {
    }
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void exit()
    {
        GameObject playerManager = GameObject.Find("InventoryManager");

        Debug.Log("PLAYER MANAGER: " + playerManager);
        Debug.Log("SHOP MANAGER: " + shopManager);


        GameObject.Find("InventoryManager").GetComponent<InventoryManager>().erase();

        for(int i = 0; i < shopManager.GetComponent<ShopManager>().inventorySlots.Length; i++)
        {
            InventorySlot slot = shopManager.GetComponent<ShopManager>().inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot != null)
            {
                GameObject.Find("InventoryManager").GetComponent<InventoryManager>().replace(itemInSlot.item, i, itemInSlot.count,shopManager.GetComponent<ShopManager>().coinCount);
            }
        }
        hasExited = true;
    }

    public bool get_hasExited()
    {
        return hasExited;
    }
    public void set_hasExited(bool val)
    {   
        hasExited = val;
    }
}

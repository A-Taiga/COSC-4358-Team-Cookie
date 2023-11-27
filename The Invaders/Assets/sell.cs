using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sell : MonoBehaviour
{
    // Start is called before the first frame update
    public Item[] sellingItems;
    public GameObject shopManager;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void sellButton()
    {

        if(shopManager.GetComponent<ShopManager>().inventorySlots[12].name == "Diamond")
        {
            print("Diamond");
            shopManager.GetComponent<ShopManager>().AddItem(sellingItems[0]);
            shopManager.GetComponent<ShopManager>().AddItem(sellingItems[0]);
            shopManager.GetComponent<ShopManager>().AddItem(sellingItems[0]);
            shopManager.GetComponent<ShopManager>().AddItem(sellingItems[0]);
        }
        // else if(inventorySlots[12].name == "Health Potion")
        // {
        //     AddItem(sellingItems[0]);
        //     AddItem(sellingItems[0]);
        // }
        // else if(inventorySlots[12].name == "Speed Potion")
        // {
        //     AddItem(sellingItems[0]);
        //     AddItem(sellingItems[0]);
        // }
        // inventorySlots[12].GetComponentInChildren<InventoryItem>().count--;
        // inventorySlots[12].GetComponentInChildren<InventoryItem>().RefreshCount();
        // if(inventorySlots[12].GetComponentInChildren<InventoryItem>().count == 0)
        // {
        //     Destroy(inventorySlots[12].GetComponentInChildren<InventoryItem>().gameObject);
        // }
    }
}

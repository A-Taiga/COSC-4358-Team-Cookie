using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject owner;
    public InventoryItem[] items;

    public virtual bool addItem(InventoryItem item, int quantity) 
    {
        if(item == null)
        {
            Debug.Log(this.name + "- The item you want to add to the inventory does not exist.");
            return false;
        }

        int index = findItem(item);
        if(index != -1)
        {
            items[index].itemQuantity += quantity;
            return true;
        } 
        else
        {
            int i = 0;
            while (i < this.items.Length)
            {
                if (InventoryItem.IsNull(this.items[i])) {
                    this.items[i] = item.Copy();
                    this.items[i].itemQuantity = quantity;
                    return true;
                }
            }
        }

        return false;
    }
    public virtual int findItem(InventoryItem item)
    {
        for(int i = 0; i < this.items.Length; i++)
        {
            if (InventoryItem.equals(this.items[i], item)) { 
                return i; 
            }
        }
        return -1;
    }
    public virtual bool RemoveItem(InventoryItem item, int quantity) 
    {
        int index = findItem(item);
        if(index != -1)
        {
            if (quantity >= items[index].itemQuantity)
            {
                this.items[index] = null;
            }
            else
            {
                this.items[index].itemQuantity -= quantity;
            }
            return true;
        }
        return false;
    }

    public virtual void ResetInventory()
    {
        this.items = new InventoryItem[items.Length];
    }

    public virtual void SetOwner(GameObject newOwner)
    {
        this.owner = newOwner;
    }
}

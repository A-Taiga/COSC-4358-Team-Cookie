using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : ScriptableObject
{
    public string itemID;

    public string itemName;
    public string itemDescription;
    public Sprite Icon;
    public GameObject Prefab;


    public bool equipableItem;
    public bool consumableItem;

    public int itemQuantity;
    public int maxStackSize;

    public AudioClip equipSound;
    public AudioClip useSound;


    public virtual InventoryItem Copy()
    {
        string name = this.itemName;
        InventoryItem clone = UnityEngine.Object.Instantiate(this) as InventoryItem;
        clone.itemName = name;
        return clone;
    }

    public static bool IsNull(InventoryItem item)
    {
        if (item == null)
        {
            return true;
        }
        if (item.itemID == null)
        {
            return true;
        }
        if (item.itemID == "")
        {
            return true;
        }
        return false;
    }

    public static bool equals(InventoryItem lhs, InventoryItem rhs)
    {
        return !IsNull(lhs) && !IsNull(rhs) && string.Equals(lhs.itemID, rhs.itemID);
    }

    public virtual void SpawnPrefab(string playerID)
    {
        GameObject droppedObject = (GameObject) Instantiate(Prefab);
    }

    public virtual bool Pick(string playerID) { return true; }

    public virtual bool Use(string playerID) { return true; }

    public virtual bool Equip(string playerID) { return true; }

    public virtual bool UnEquip(string playerID) { return true; }

    public virtual void Swap(string playerID) { }

    public virtual bool Drop(string playerID) { return true; }
}

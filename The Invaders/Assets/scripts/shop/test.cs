using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    public InventoryManager manager;

    void Start()
    {
        // manager = FindObjectOfType<Canvas>().GetComponentInChildren<InventoryManager>();
        manager = GameObject.Find("Canvas").GetComponentInChildren<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(manager.inventorySlots[0].transform.GetChild(0).GetComponent<InventoryItem>().item.itemName);
    }
}

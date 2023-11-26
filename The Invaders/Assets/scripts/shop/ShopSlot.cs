using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSlot : InventorySlot
{
    // Start is called before the first frame update

    public void Awake()
    {
        // canvas = FindObjectOfType<Canvas>().gameObject;
        // canvas = GameObject.Find("ShopCanvas");
		// inventoryManager = FindObjectOfType<ShopManager>();
		Deselect();
    }
    void Start()
    {
        // canvas = FindObjectOfType<Canvas>().gameObject;
        canvas = GameObject.Find("ShopCanvas");
		inventoryManager = FindObjectOfType<ShopManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

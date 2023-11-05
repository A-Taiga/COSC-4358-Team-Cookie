using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldItem : MonoBehaviour
{


    [SerializeField]
    Item item;
    [SerializeField]
    public SpriteRenderer spriteRenderer;
    [SerializeField]
    public InventoryManager inventoryManager;
    void Start()
    {
        spriteRenderer.sprite = item.image;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            inventoryManager.AddItem(item);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldItem : MonoBehaviour
{


    [SerializeField]
    public Item item;
    [SerializeField]
    public SpriteRenderer spriteRenderer;
    [SerializeField]
    public InventoryManager inventoryManager;
    void Start()
    {
        spriteRenderer.sprite = item.image;
        inventoryManager = GameObject.Find("Canvas").GetComponentInChildren<InventoryManager>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(item.name == "sword")
            {
                collision.gameObject.GetComponent<Animator>().SetTrigger("PickedUpSword");
                inventoryManager.hasSword = true;
                Destroy(gameObject);
                return;
            }
            inventoryManager.AddItem(item);
            Destroy(gameObject);
        }
    }
}

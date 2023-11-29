using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
                Player player = Player.getPlayerObject().GetComponent<Player>();
                if (player.progress < 4)
                {
                    player.progress = 4;
                    player.GetComponentInChildren<PopupMessage>().ShowPopup("I can now enter the Volcano Islands!", 5f);
                }

                collision.gameObject.GetComponent<Animator>().SetTrigger("PickedUpSword");
                inventoryManager.hasSword = true;
                SaveManager.Instance.SaveData(inventoryManager);
                Destroy(gameObject);
                return;
            } 
            else if (item.name == "shield")
            {
                Player player = Player.getPlayerObject().GetComponent<Player>();
                if (player.progress < 5)
                {
                    player.progress = 5;
                    player.GetComponentInChildren<PopupMessage>().ShowPopup("I can now enter the North Beach!", 5f);
                }
            }
            inventoryManager.AddItem(item);
            Destroy(gameObject);
        }
    }
}

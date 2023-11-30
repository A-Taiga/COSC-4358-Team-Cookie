using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ForestVillager : MonoBehaviour
{
    public Sprite healedSprite;
    public static bool injured = true;
    public static bool scared = true;
    public Dialogue savedDialog;
    public Item potionItem;
    
    public Item[] drops;
    public GameObject worldItem;
    
    IEnumerator Start()
    {
        if (!injured && !scared)
        {
            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
            sr.sprite = healedSprite;
        }
        
        Events<EndDialogue>.Instance.Register(EndCheck);
        while (true)
        {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            
            if (enemies.Length <= 0)
            {
                scared = false;
                break;
            }
            yield return new WaitForSeconds(2f);
        }
    }

    void EndCheck(string name)
    {
        if (name.Equals("forest_injured_npc"))
        {
            DropItems();
            Player player = Player.getPlayerObject().GetComponent<Player>();
            player.progress = 2;
            player.gameObject
                .GetComponentInChildren<PopupMessage>()
                .ShowPopup("Maybe I should check out Sunset Bay...", 5f);
            SaveManager.Instance.SaveData(player);
            
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!injured)
        {
            if (savedDialog.dialogSeen)
            {
                this.gameObject.GetComponentInChildren<PopupMessage>().ShowPopup("Thank you once again for saving me. They keep coming back...", 3f);
            }
        }
        else
        {
            if (!scared)
            {
                InventoryItem potion = null;
                
                for(int i = 0; i < InventoryManager.Instance.inventorySlots.Length; i++)
                {
                    Debug.Log("yo");
                    InventoryItem itemInSlot = InventoryManager.Instance.inventorySlots[i]
                        .GetComponentInChildren<InventoryItem>();
                    if(itemInSlot != null && itemInSlot.item && itemInSlot.item == potionItem)
                    {
                        itemInSlot.count--;
                        itemInSlot.RefreshCount();
                        potion = itemInSlot;
                    }
                }
                if (potion != null)
                {
                    Debug.Log("WE HAVE POTION");
                    SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
                    sr.sprite = healedSprite;
                    Events<StartDialogue>.Instance.Trigger?.Invoke("forest_injured_npc");
                    injured = false;
                    
                    if (potion.count <= 0)
                    {
                        Destroy(potion.gameObject);
                    }
                }
                else
                {
                    this.gameObject.GetComponentInChildren<PopupMessage>().ShowPopup("Everything hurts!!! I need a potion please...", 3f);
                }
            }
            else
            {
                this.gameObject.GetComponentInChildren<PopupMessage>().ShowPopup("Help me... I'm scared..", 3f);
            }
        }
    }

    void DropItems()
    {
        if (!worldItem)
            return;
        
        foreach(var item in drops)
        {
            Vector2 offsets = Random.insideUnitCircle.normalized * 0.16f;
            worldItem.GetComponent<WorldItem>().item = item;
            Instantiate(worldItem, (Vector2) gameObject.transform.position + offsets, Quaternion.identity);
        }
    }
}

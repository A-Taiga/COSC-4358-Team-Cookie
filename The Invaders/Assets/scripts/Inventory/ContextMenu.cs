using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ContextMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    
    [SerializeField]
    public Button useButton;
    public Button splitButton;
    public Button discardButton;

    public GameObject item;


    public bool mouseExit = false;

    public void Update()
    {   
        if(mouseExit == true)
        {
            Destroy(gameObject);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseExit = false;
        Debug.Log("Mouse enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseExit = true;
        Debug.Log("Mouse exit");
        Destroy(gameObject);
    }

    public void UseButton()
    {
        if(item.gameObject.GetComponent<InventoryItem>().item.type == ItemType.Consumable)
        {

            if(item.gameObject.GetComponent<InventoryItem>().item.name == "SpeedPotion")
            {
                Debug.Log("Speed Potion");
                item.gameObject.GetComponent<InventoryItem>().count--;
                item.gameObject.GetComponent<InventoryItem>().RefreshCount();
                GameObject go = GameObject.Find("Stamina");
                StaminaBar other = (StaminaBar) go.GetComponent(typeof(StaminaBar));
                Debug.Log("GO: " + go);
                Debug.Log("OTHER: " + other);
                other.AddHealth(50);
            }
            else
            {
                item.gameObject.GetComponent<InventoryItem>().count--;
                item.gameObject.GetComponent<InventoryItem>().RefreshCount();
                GameObject go = GameObject.Find("Healthbar");
                HealthBar other = (HealthBar) go.GetComponent(typeof(HealthBar));
                other.AddHealth(20);
            }
           

            if(item.gameObject.GetComponent<InventoryItem>().count == 0)
            {
                Destroy(item);
            }
            Destroy(gameObject);
        }
    }

    public void SplitButton()
    {
        Debug.Log("NOT YET IMPLEMENTED");
        Destroy(gameObject);
    }

    public void DiscardButton()
    {
        item.gameObject.GetComponent<InventoryItem>().count--;
        item.gameObject.GetComponent<InventoryItem>().RefreshCount();
        Debug.Log("Discard 1");
        if(item.gameObject.GetComponent<InventoryItem>().count == 0)
        {
            Destroy(item);
        }
        Destroy(gameObject);
    }
}


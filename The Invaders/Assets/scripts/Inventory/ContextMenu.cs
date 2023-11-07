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


    public bool mouseExit = true;

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

            item.gameObject.GetComponent<InventoryItem>().count--;
            item.gameObject.GetComponent<InventoryItem>().RefreshCount();
            GameObject go = GameObject.Find("Healthbar");
            HealthBar other = (HealthBar) go.GetComponent(typeof(HealthBar));
            other.AddHealth(20);

            if(item.gameObject.GetComponent<InventoryItem>().count == 0)
            {
                Destroy(item);
            }
            Destroy(gameObject);
        }
    }

    public void SplitButton()
    {
        Destroy(gameObject);
    }

    public void DiscardButton()
    {
        Debug.Log("Discard");
        Destroy(item);
        // Destroy();
    }
}


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

    private bool mouse_over = false;

    public GameObject item;


    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
        Debug.Log("Mouse enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
        Debug.Log("Mouse exit");
        Destroy(gameObject);
    }

    public void UseButton()
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


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
        var _item = item.gameObject.GetComponent<InventoryItem>();
        if(_item.item.type == ItemType.Consumable)
        {

            if(_item.item.name == "SpeedPotion")
            {
                Debug.Log("Speed Potion");
                _item.count--;
                _item.RefreshCount();
                GameObject go = GameObject.Find("Stamina");
                StaminaBar other = (StaminaBar) go.GetComponent(typeof(StaminaBar));
                other.AddHealth(75);
            }
            else
            {
                _item.count--;
                _item.RefreshCount();
                GameObject go = GameObject.Find("Healthbar");
                HealthBar other = (HealthBar) go.GetComponent(typeof(HealthBar));
                other.AddHealth(20);
            }

            if (_item.item.useSound)
            {
                var src = gameObject.transform.root.gameObject.GetComponent<AudioSource>();
                if (src)
                {
                    src.clip = _item.item.useSound;
                    src.Play();
                }
            }
            
            if(_item.count == 0)
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


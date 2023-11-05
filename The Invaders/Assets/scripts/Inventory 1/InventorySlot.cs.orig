using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerDownHandler
{
    public Image image;
    public Color selectedColor, notSelectedColor;
    public InventoryManager inventoryManager;
    public int slotIndex;

    public void Awake()
    {
        Deselect();
    }
    public void Select()
    {
        image.color = selectedColor;
    }
    public void Deselect()
    {
        image.color = notSelectedColor;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        InventoryItem inventoryItem = transform.GetChild(0).gameObject.GetComponent<InventoryItem>();
        if(inventoryItem != null)
        {
            Debug.Log("NAME: " + inventoryItem.item.name + " SLOT INDEX: " + slotIndex);
            inventoryManager.ChangeSelectedSlot(slotIndex);
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;
        }
    }

}

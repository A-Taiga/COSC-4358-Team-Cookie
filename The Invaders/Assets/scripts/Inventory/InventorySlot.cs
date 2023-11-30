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

	public GameObject canvas;
	public int slotIndex;
    public ContextMenu contextMenu;

	public virtual void Awake()
	{
		canvas = FindObjectOfType<Canvas>().gameObject;
		inventoryManager = FindObjectOfType<InventoryManager>();
		Deselect();
	}
	public void Select()
	{
		if(image != null)
			image.color = selectedColor;
	}
	public void Deselect()
	{
		if(image != null)
			image.color = notSelectedColor;
	}
	public void OnPointerDown(PointerEventData eventData)
	{
		InventoryItem inventoryItem = GetComponentInChildren<InventoryItem>();
		if(inventoryItem != null)
		{
			
			//Debug.Log("BUTTON: " + eventData.button);
			if(eventData.button == PointerEventData.InputButton.Right)
			{
				var obj = Instantiate(contextMenu, Input.mousePosition, Quaternion.identity, transform.parent.parent);
				obj.item = transform.GetChild(0).gameObject;
			}
			else if(eventData.button == PointerEventData.InputButton.Left)
			{
				inventoryManager.ChangeSelectedSlot(slotIndex);
			}
			// Debug.Log("NAME: " + inventoryItem.item.name + " SLOT INDEX: " + slotIndex);
			inventoryManager.ChangeSelectedSlot(slotIndex);
		}
	}

	
	public void OnDrop(PointerEventData eventData)
	{
		InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
		if(transform.childCount == 0)
		{
			inventoryItem.parentAfterDrag = transform;
			inventoryManager.ChangeSelectedSlot(slotIndex);
		}
		/* don't ask why i don't just use a variable fot the event data and inventoryItem, it doesn't work and i don't know why (it makes unity crash) */
		else if(transform.GetChild(0).gameObject.GetComponent<InventoryItem>().item.name == eventData.pointerDrag.GetComponent<InventoryItem>().item.name)
		{
			while(transform.GetChild(0).gameObject.GetComponent<InventoryItem>().count < inventoryManager.maxStackedItems && eventData.pointerDrag.GetComponent<InventoryItem>().count > 0)
			{
				transform.GetChild(0).gameObject.GetComponent<InventoryItem>().count++;
				eventData.pointerDrag.GetComponent<InventoryItem>().count--;
				transform.GetChild(0).gameObject.GetComponent<InventoryItem>().RefreshCount();
				eventData.pointerDrag.GetComponent<InventoryItem>().RefreshCount();
			}
			if(eventData.pointerDrag.GetComponent<InventoryItem>().count <= 0)
			{
				Destroy(eventData.pointerDrag.GetComponent<InventoryItem>().gameObject);
			}
		}
	}
}
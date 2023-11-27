using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    public GameObject shopManager;
    public Item item;

    public bool selected = false;
    public int price;
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    public void OnPointerClick(PointerEventData eventData) // 3
    {
        if(shopManager.GetComponent<ShopManager>().coinCount < price)
            return;
        shopManager.GetComponent<ShopManager>().AddItem(item);
        shopManager.GetComponent<ShopManager>().coinCount -= price;
        shopManager.GetComponent<ShopManager>().updateCoinCount();
    }


}

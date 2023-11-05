using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static bool visible;
    public GUIController uiController;
    // Update is called once per frame
    void Start()
    {
        visible = false;
    }
    void Update()
    {
        Debug.Log("InventoryController.Update");
        if (Input.GetButtonDown("Inv"))
        {
            if (visible)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
    }
    public void Hide()
    {
        visible = false;
        uiController.SetInventoryScreen(false);
    }
    public void Show()
    {
        visible = true;
        uiController.SetInventoryScreen(true);
    }
}

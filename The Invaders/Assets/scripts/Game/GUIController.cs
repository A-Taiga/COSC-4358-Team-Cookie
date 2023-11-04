using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class GUIController : MonoBehaviour
{
    /// the main canvas
    [Tooltip("the main canvas")]
    public Canvas MainCanvas;
    /// the game object that contains the heads up display (avatar, health, points...)
    [Tooltip("the game object that contains the heads up display (avatar, health, points...)")]
    public GameObject HUD;
    /// the pause screen game object
    [Tooltip("the pause screen game object")]
    public GameObject PauseScreen;
    /// the death screen
    [Tooltip("the death screen")]
    public GameObject DeathScreen;

    [Tooltip("the inventory screen")]
    public GameObject InventoryScreen;
    
    // Start is called before the first frame update
    void Start()
    {
        SetPauseScreen(false);
        SetDeathScreen(false);
        SetInventoryScreen(false);
    }
    void Update()
    {
        if (Input.GetButtonDown("Inv"))
        {
            if (InventoryController.visible)
            {
                SetInventoryScreen(false);
            }
            else
            {
                SetInventoryScreen(true);
            }
        }
    }

    public virtual void SetHUDActive(bool state)
    {
        if (HUD != null)
        {
            HUD.SetActive(state);
        }
    }

    public virtual void SetPauseScreen(bool state)
    {
        if (PauseScreen != null)
        {
            PauseScreen.SetActive(state);
            EventSystem.current.sendNavigationEvents = state;
        }
    }

    public virtual void SetDeathScreen(bool state)
    {
        if (DeathScreen != null)
        {
            DeathScreen.SetActive(state);
            EventSystem.current.sendNavigationEvents = state;
        }
    }

    public virtual void SetInventoryScreen(bool state)
    {
        if (InventoryScreen != null)
        {
            InventoryScreen.SetActive(state);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WiseManEnd : MonoBehaviour
{
    private Player player;

    void Start()
    {
        player = Player.getPlayerObject().GetComponent<Player>();
    }
    public void EndCheck(string npc)
    {
        if (npc.Equals("wizard_npc"))
        {
            if (player.progress < 3)
            {
                player.progress = 3;
                player.GetComponentInChildren<PopupMessage>().ShowPopup("I can now enter the Wetlands!", 5f);
            }
        }
    }

    void OnEnable()
    {
        Events<EndDialogue>.Instance.Register(EndCheck);
    }

    private void OnDisable()
    {
        Events<EndDialogue>.Instance.Unregister(EndCheck);
    }
}
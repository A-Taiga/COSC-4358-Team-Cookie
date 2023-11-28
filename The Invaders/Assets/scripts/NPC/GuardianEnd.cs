using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GuardianEnd : MonoBehaviour
{
    private Player player;

    void Start()
    {
        player = Player.getPlayerObject().GetComponent<Player>();
    }
    public void EndCheck(string npc)
    {
        if (npc.Equals("guardian_npc"))
        {
            if (player.progress < 1)
            {
                player.progress = 1;
                player.GetComponentInChildren<PopupMessage>().ShowPopup("I can now enter the Enchanted Forest!", 5f);
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

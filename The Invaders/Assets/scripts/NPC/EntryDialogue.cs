using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryDialogue : MonoBehaviour
{
    public Dialogue dialogue;

    void FixedUpate()
    {
        if (!dialogue.started)
        {
            Debug.Log("Starting " + dialogue.dialogIdentifier);
            dialogue.DialogueCheck(dialogue.dialogIdentifier);
            Destroy(this);
        }
    }
}

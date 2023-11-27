using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColTrigger : MonoBehaviour
{
    public string triggerName;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Events<StartDialogue>.Instance.Trigger?.Invoke(triggerName);
    }
}

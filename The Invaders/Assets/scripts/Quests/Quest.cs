using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Quest : MonoBehaviour
{
    public bool isActive;

    public string questName;
    public string questTitle;
    public string questDescription;
    public string questType;

    public string itemReward;
    public int goldReward;

}

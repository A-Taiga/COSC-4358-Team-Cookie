using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemInfo : MonoBehaviour
{


    public Image image;
    public TMP_Text itemName;
   public void DisplaySelected(Image i, string name)
   {
        image.sprite = i.sprite;
        image.preserveAspect = true;
        itemName.text = name;
   }
}

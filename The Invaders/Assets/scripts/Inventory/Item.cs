using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

 public enum ItemType
    {
        Weapon,
        Consumable,
        Key,
        Other
    }
    public enum ActionType
    {
        Attack,
        Heal,
        Other
    }


[CreateAssetMenu(menuName = "Scriptable objects/Item")]
public class Item : ScriptableObject
{
    [Header("Only gameplay")]


    public string itemName;
    public TileBase tile;
    public ItemType type;
    public ActionType actionType;
    public Vector2Int range = new Vector2Int(5,4);

    [Header("Only UI")]
    public bool stackable = true;

    [Header("Both")]
    public Sprite image;


   
}

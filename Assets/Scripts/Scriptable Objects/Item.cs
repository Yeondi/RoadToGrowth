using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject
{
    public string objectName;
    public Sprite sprite;
    public int quantity;
    public int price;
    public bool Stackable;
    public enum ItemType
    {
        COIN,
        HEALTH,
        ATK_BUFF,
        DF_BUFF,
        END,
        CHANGE_HERO
    }

    public ItemType itemType;
    public int itemTypeSize = System.Enum.GetValues(typeof(ItemType)).Length;
}

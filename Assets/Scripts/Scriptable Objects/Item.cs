using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject
{
    public string objectName;
    public Sprite sprite;
    public int quantity;
    public bool Stackable;
    public enum ItemType
    {
        COIN,
        HEALTH
    }

    public ItemType itemType;
}

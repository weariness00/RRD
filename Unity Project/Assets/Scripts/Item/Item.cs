using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ItemType
{
    Sword,
    Staff,
    Bow
};

[System.Serializable]
public enum ItemRate
{
    Normal,
    Magic,
    Unique,
    Legendry
};

[System.Serializable]
public class ItemInfo
{
    public int id;
    public string name;
    public ItemType type;
    public ItemRate rate;
    public float dropChance;
}

public class Item : MonoBehaviour
{
    public int id;
    public float dropChance;

    [Space]
    public ItemType type;
    public ItemRate rate;

    public Item(ItemInfo info)
    {
        id = info.id;
        name = info.name;
        dropChance = info.dropChance;
    }
    
}

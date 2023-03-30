using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public string dropMonster;
    public float dropChance;
}

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/Item", order = int.MaxValue)]
public class Item : ScriptableObject
{
    public int id;
    public GameObject prefab;
    public Sprite icon;

    public float dropChance;
    public int amount;

    [Space]
    public ItemType type;
    public ItemRate rate;

    public Item(ItemInfo info)
    {
        id = info.id;
        name = info.name;
        dropMonster = info.dropMonster;
        dropChance = info.dropChance;
    }
    
}

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
    Nomal,
    Magic,
    Unique
};

[System.Serializable]
public class ItemInfo
{
    public int id;
    public string name;

    [Space]
    public ItemType type;
    public ItemRate rate;

    [Space]
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
}
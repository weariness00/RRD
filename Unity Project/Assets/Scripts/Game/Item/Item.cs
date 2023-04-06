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
    Random,
    Normal,
    Magic,
    Unique,
    Legendry
};

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/Item", order = int.MaxValue)]
public class Item : ScriptableObject
{
    public int id;
    public GameObject prefab;
    public Sprite icon;

    public int amount;

    [Space]
    public ItemType type;
    public ItemRate rate;

    public Item(ItemDropInfo info)
    {
        name = info.name;
    }
}

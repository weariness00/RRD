using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    public Define.ItemTear tear;

    public UnityEvent AbilityCall;

    public Item(ItemDropInfo info)
    {
        name = info.name;
    }
}

//public class Item : MonoBehaviour
//{
//    static protected int idCount = 0;

//    public int id;
//    public Sprite icon;

//    public Define.ItemTear tear;

//    public int amount;

//    protected virtual void Init()
//    {
//        id = idCount++;
//    }
//}
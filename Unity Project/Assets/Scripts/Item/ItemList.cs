using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    public static ItemList Instance;
    
    public List<Item> itemList;

    private void Awake()
    {
        Instance = this;
    }

    public Item GetItem(string name)
    {
        foreach(Item item in itemList)
        {
            if (item.name.Equals(name))
                return item;
        }

        return null;
    }

    public Item GetItme(int index) { return itemList[index]; }
}

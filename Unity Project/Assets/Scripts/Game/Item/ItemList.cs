using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    public static ItemList Instance;
    
    public List<ItemData> itemList;

    private void Awake()
    {
        Instance = this;
    }

    public ItemData GetItem(string name)
    {
        foreach(ItemData itemData in itemList)
        {
            if (itemData.name.Equals(name))
                return itemData;
        }

        return null;
    }

    public ItemData GetItme(int index) { return itemList[index]; }
}

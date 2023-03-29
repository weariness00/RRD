using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    public ItemData itemData;
    public static ItemList Instance;

    private void Awake()
    {
        Instance = this;   
    }

    /*public ItemInfo GetSelectItem(string itemName, ItemRate itemRate)
    {
        foreach(var data in itemData.ItemSheet)
        {
            if(data.name == itemName && data.rate == itemRate)
                return data;
        }
        return null;
    }*/

    //선택한 열에 있는 아이템만 담아옴
    public ItemInfo GetSelectItem(int index)
    {
        return itemData.ItemSheet[index];
    }

    //담아온 아이템들 중에서 드랍율을 넘은애만 다시 담아줌
    public ItemInfo RandomDrop()
    {
        int chance = Random.Range(0, 100);
        foreach(var data in itemData.ItemSheet)
        {
            if (data.dropChance <= chance)
                return data;
        }
        return null; 
    }
}

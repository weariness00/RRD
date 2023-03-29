using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemList : MonoBehaviour
{
    public ItemData itemData;
    public static ItemList Instance;
    public List<Item> itemList;

    private void Awake()
    {
        Instance = this;
    }

    public List<Item> MakeDropTable(List<Item> itemList, string monsterName)
    {
        int index = 0;
        for (int i = 0; i < itemData.ItemSheet.Count; ++i)
        {
            if(itemData.ItemSheet[index].dropMonster == monsterName)  //이 부분 검사를 어떻게 할지가 정말 애매한거같은데
            {
                itemList[index] = itemData.ItemSheet[i];
                itemList[index] = RandomDrop(itemList[index]);
                
                if (itemList[index] != null)
                    index++;
            }
        }
        return itemList;
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
    public Item GetSelectItem(int index)  //사용처가 있나?
    {
        return itemData.ItemSheet[index];
    }

    //담아온 아이템들 중에서 드랍율을 넘은애만 다시 담아줌
    public Item RandomDrop(Item data)
    {
        int chance = Random.Range(0, 100);
        if (data.dropChance <= chance)
            return data;
        return null; 
    }



}

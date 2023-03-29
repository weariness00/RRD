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
            if(itemData.ItemSheet[index].dropMonster == monsterName)  //�� �κ� �˻縦 ��� ������ ���� �ָ��ѰŰ�����
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

    //������ ���� �ִ� �����۸� ��ƿ�
    public Item GetSelectItem(int index)  //���ó�� �ֳ�?
    {
        return itemData.ItemSheet[index];
    }

    //��ƿ� �����۵� �߿��� ������� �����ָ� �ٽ� �����
    public Item RandomDrop(Item data)
    {
        int chance = Random.Range(0, 100);
        if (data.dropChance <= chance)
            return data;
        return null; 
    }



}

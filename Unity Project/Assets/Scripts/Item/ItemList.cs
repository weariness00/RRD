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

    //������ ���� �ִ� �����۸� ��ƿ�
    public ItemInfo GetSelectItem(int index)
    {
        return itemData.ItemSheet[index];
    }

    //��ƿ� �����۵� �߿��� ������� �����ָ� �ٽ� �����
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

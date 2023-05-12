using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
	public List<ItemData> itemDatas = new List<ItemData>();
	public List<ItemData> itemDatassssss;

	GameObject content;

	[SerializeField] GameObject itemUIPrefab;

    private void Start()
	{
		content = Util.FindChild<ScrollRect>(gameObject).content.gameObject;
		SetInventory(itemDatassssss);	// 임시용
    }

	public void AddItem(ItemData itemdata)
	{
		if (itemDatas.Exists(_Item => {return _Item.id == itemdata.id; } ))
		{
			// 중복 소지시 어떻게 할지
			int index = itemDatas.FindIndex((_Item) => { return _Item == itemdata; });
            itemDatas[index].amount += itemdata.amount;
		}
		else
		{
			itemDatas.Add(itemdata);
            itemDatas.Sort((a, b) => { return a.id < b.id ? -1 : 1; });

			GameObject itemNode = Instantiate(itemUIPrefab, content.transform);
			Util.FindChild<Image>(itemNode, "Icon").GetComponent<Image>().sprite = itemdata.icon;
			content.transform.SetSiblingIndex(itemDatas.IndexOf(itemdata));
        }
    }

	public void RemoveItem(int index)
	{
		if (itemDatas.Count.Equals(0))
			return;
		itemDatas.RemoveAt(index);
		Destroy(content.transform.GetChild(index).gameObject);
	}

	public void UseItem(ItemData itemData)
	{
		itemData.amount--;
		if(itemData.amount.Equals(0))
		{
			RemoveItem(itemDatas.FindIndex(_ItemData => { return itemData.id == _ItemData.id; }));
			return;
		}	
	}

	// 이 인벤에 대한 정보가 있을경우 그 정보로 인벤토리 재생성
	public void SetInventory(List<ItemData> _Items)
	{
        _Items.ForEach((item) => { AddItem(item); });
    }
}

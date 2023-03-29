using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
	public List<Item> items = new List<Item>();
	public List<Item> itemssssss;

	GameObject content;

	[SerializeField] GameObject itemUIPrefab;

    private void Start()
	{
		content = Util.FindChild<ScrollRect>(gameObject).content.gameObject;
		SetInventory(itemssssss);	// �ӽÿ�
    }

	public void AddItem(Item item)
	{
		if (items.Exists(_Item => {return item.id == _Item.id; } ))
		{
			// �ߺ� ������ ��� ����
			int index = items.FindIndex((_Item) => { return _Item == item; });
			items[index].amount += item.amount;
		}
		else
		{
			items.Add(item);
            items.Sort((a, b) => { return a.id < b.id ? -1 : 1; });

			GameObject itemNode = Instantiate(itemUIPrefab, content.transform);
			Util.FindChild<Image>(itemNode, "Icon").GetComponent<Image>().sprite = item.icon;
			content.transform.SetSiblingIndex(items.IndexOf(item));
        }
    }

	public void RemoveItem(int index)
	{
		items.RemoveAt(index);
		Destroy(content.transform.GetChild(index).gameObject);
	}

	public void UseItem(Item item)
	{
		item.amount--;
		if(item.amount.Equals(0))
		{
			RemoveItem(items.FindIndex(_Item => { return item.id == _Item.id; }));
			return;
		}	
	}

	// �� �κ��� ���� ������ ������� �� ������ �κ��丮 �����
	public void SetInventory(List<Item> _Items)
	{
        _Items.ForEach((item) => { AddItem(item); });
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct objectinfo
{
	public int id;
}

public struct Block
{
	public Image icon;
	public objectinfo info;
	public int count;
}

public class Inventory : MonoBehaviour
{
	Canvas canvas;
	
	public List<Block> blocks = new List<Block>();

    private void Start()
    {
        canvas = GetComponent<Canvas>();
    }

	public void SortItem()
	{
		blocks.Sort();

		GameObject content = Util.FindChild(gameObject, "content");
		foreach (GameObject obj in Util.GetChildren(content))
		{
			Destroy(obj);
		}

		foreach (var item in blocks)
		{
			Instantiate(item.icon, content.transform);
		}
	}

	public void AddItem(Block block)
	{
		blocks.Add(block);
		SortItem();
	}

	public void RemoveItem(int index)
	{
		blocks.RemoveAt(index);
		SortItem();
	}

	public void UseItem()
	{

	}
}

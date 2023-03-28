using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public struct ItemNode
{
	public Image icon;
	public int id;
	public int count;
}

public class UIInventory : MonoBehaviour
{
	public List<ItemNode> blocks = new List<ItemNode>();

	GameObject content;

    private void Start()
	{
		content = Util.FindChild<ScrollRect>(gameObject).content.gameObject;

		//임시용
		foreach (var block in Util.GetChildren(content))
		{
            ItemNode newBlock = new ItemNode { icon = block.GetComponent<Image>() };
            blocks.Add(newBlock);
		}
	}

	public void AddItem(ItemNode block)
	{
		if(blocks.Contains(block))
		{
			// 중복 소지시 어떻게 할지
		}
		else
		{
			blocks.Add(block);
            blocks.Sort((a, b) => { return a.id < b.id ? -1 : 1; });

			Instantiate(block.icon, content.transform);
			content.transform.SetSiblingIndex(blocks.IndexOf(block));
        }
    }

	public void RemoveItem(int index)
	{
		blocks.RemoveAt(index);
		Destroy(content.transform.GetChild(index).gameObject);
	}

	public void UseItem()
	{

	}

	// 이 인벤에 대한 정보가 있을경우 그 정보로 인벤토리 재생성
	public void SetInventory()
	{

	}
}

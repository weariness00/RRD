using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropTable : MonoBehaviour
{
	public List<Item> items;

	public void SetDropItem(List<Item> _Items)
    {
        items = _Items;
    }

    public void Loot()
    {
        items.ForEach((item) => { Instantiate(item.prefab); });
    }

    public void TestLoot()
    {
        items = LootingSystem.Instance.SetDropTable(gameObject.GetComponent<Monster>());
    }
}

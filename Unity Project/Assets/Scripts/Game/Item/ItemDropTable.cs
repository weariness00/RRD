using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropTable : MonoBehaviour
{
	public List<ItemData> itemDatas;

    public void SetDropItem(List<ItemData> _ItemDatas)
    {
        itemDatas = _ItemDatas;
    }

    public void Loot()
    {
        itemDatas.ForEach((itemData) => { Instantiate(itemData.prefab, gameObject.transform.position, Quaternion.identity); });
    }

    public void TestLoot()
    {
        itemDatas = LootingSystem.Instance.SetDropTable(gameObject.GetComponent<Monster>());
    }
}

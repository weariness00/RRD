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
        itemDatas.ForEach((itemData) => {
            if (itemData.prefab == null) return;
            GameObject obj = Instantiate(itemData.prefab, gameObject.transform.position, Quaternion.identity);
            obj.transform.position += Vector3.up;

            Item item = obj.GetComponent<Item>();
            item.StartCoroutine(item.InitRigidBody());
        });
    }

    public void TestLoot()
    {
        itemDatas = LootingSystem.Instance.SetDropTable(gameObject.GetComponent<Monster>());
    }
}

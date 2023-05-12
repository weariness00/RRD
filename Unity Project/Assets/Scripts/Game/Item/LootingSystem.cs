using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DropTable
{
    public ItemData itemData;
    public float chance;
}

[System.Serializable]
public class ItemDropInfo
{
    public string name;
    public ItemRate rate;
    public MonsterRate monsterRate;
    public string dropMonster;
    public float dropChance;
}

public class LootingSystem : MonoBehaviour
{
    public static LootingSystem Instance;
    public ItemDropData itemDropData;

    private void Awake()
    {
        Instance = this;
    }

    public ItemData CheckDropChance(DropTable table)
    {
        int chance = Random.Range(0, 100);
        if (table.chance >= chance)
            return table.itemData;

        return null;
    }

    public List<ItemData> SetDropTable(Monster monster)
    {
        List<ItemData> items = new List<ItemData>();

        for (int i = 0; i < itemDropData.ItemSheet.Count; ++i)
        {
            if (itemDropData.ItemSheet[i].dropMonster == monster.name &&
                itemDropData.ItemSheet[i].monsterRate == monster.rate)  //이 부분 검사를 어떻게 할지가 정말 애매한거같은데
            {
                DropTable dt = new DropTable();
                dt.itemData = ItemList.Instance.GetItem(itemDropData.ItemSheet[i].name); // 임시
                dt.chance = itemDropData.ItemSheet[i].dropChance;

                ItemData itemData = CheckDropChance(dt);
                if(itemData != null)
                    items.Add(itemData);
            }
        }

        return items;
    }
}

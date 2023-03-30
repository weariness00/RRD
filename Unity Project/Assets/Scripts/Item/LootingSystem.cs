using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DropTable
{
    public Item item;
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
    public ItemData itemDropData;

    private void Awake()
    {
        Instance = this;
    }

    public Item CheckDropChance(DropTable table)
    {
        int chance = Random.Range(0, 100);
        if (table.chance >= chance)
            return table.item;

        return null;
    }

    public List<Item> SetDropTable(Monster monster)
    {
        List<Item> items = new List<Item>();

        for (int i = 0; i < itemDropData.ItemSheet.Count; ++i)
        {
            if (itemDropData.ItemSheet[i].dropMonster == monster.name &&
                itemDropData.ItemSheet[i].monsterRate == monster.rate)  //이 부분 검사를 어떻게 할지가 정말 애매한거같은데
            {
                DropTable dt = new DropTable();
                dt.item = ItemList.Instance.GetItem(itemDropData.ItemSheet[i].name); // 임시
                dt.chance = itemDropData.ItemSheet[i].dropChance;

                Item item = CheckDropChance(dt);
                if(item != null)
                    items.Add(item);
            }
        }

        return items;
    }
}

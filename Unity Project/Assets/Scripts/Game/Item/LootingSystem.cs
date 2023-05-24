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
            if (itemDropData.ItemSheet[i].dropMonster == monster.name )
            {
                DropTable dt = new DropTable();
                dt.itemData = ItemList.Instance.GetItem(itemDropData.ItemSheet[i].name); // юс╫ц
                dt.chance = itemDropData.ItemSheet[i].dropChance;

                ItemData itemData = CheckDropChance(dt);
                if(itemData != null)
                    items.Add(itemData);
            }
        }

        return items;
    }
}

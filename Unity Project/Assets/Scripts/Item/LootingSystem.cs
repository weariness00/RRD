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
                itemDropData.ItemSheet[i].monsterRate == monster.rate)  //�� �κ� �˻縦 ��� ������ ���� �ָ��ѰŰ�����
            {
                DropTable dt = new DropTable();
                dt.item = ItemList.Instance.GetItem(itemDropData.ItemSheet[i].name); // �ӽ�
                dt.chance = itemDropData.ItemSheet[i].dropChance;

                Item item = CheckDropChance(dt);
                if(item != null)
                    items.Add(item);
            }
        }

        return items;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


class etc: Component { }

public class LootingSystem : MonoBehaviour
{
    public static LootingSystem Instance;
    public List<GameObject> itemPrefab;  //후에 ItemManager로 이동
    public List<Item> itemList;
    public string monsterName;

    private void Awake()
    {
        Instance = this;
        itemPrefab = new List<GameObject>();
        itemList = ItemList.Instance.MakeDropTable(itemList, monsterName);
    }

    public void Loot()
    {
        for (int i = 0; i < itemPrefab.Count; ++i)
        {
            ItemDropTable idt = Util.GetORAddComponet<ItemDropTable>(itemPrefab[i]);
            for (int index = 0; index < idt.items.Length; index++)
            {
                if (itemList.Contains(idt.items[index]))
                    Instantiate(itemPrefab[i], transform.position, Quaternion.identity);
            }
        }
    }
}

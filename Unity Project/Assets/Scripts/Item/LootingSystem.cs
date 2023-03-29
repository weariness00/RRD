using System.Collections;
using System.Collections.Generic;
using UnityEngine;




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
            Util.GetORAddComponet<Item>(itemPrefab[i]);
            for (int index = 0; index < itemList.Count; index++)
            {
                if (itemPrefab[i].GetComponent<Item>() == itemList[index])
                    Instantiate(itemPrefab[i], transform.position, Quaternion.identity);
            }
        }
    }
}

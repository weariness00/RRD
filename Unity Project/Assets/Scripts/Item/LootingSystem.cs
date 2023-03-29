using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootingSystem : MonoBehaviour
{
    public static LootingSystem Instance;
    public ItemData itemData;
    public List<ItemInfo> itemList;
    public GameObject itemPrefab;

    private void Awake()
    {
        Instance = this;
        Util.GetORAddComponet<Item>(itemPrefab);
    }

    

    public void Loot()
    {
        foreach (var item in itemData.ItemSheet)  //여기 그냥 리스트 길이만 알아도 될거 같기도 하고
        {
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
        }
    }
}

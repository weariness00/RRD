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
        foreach (var item in itemData.ItemSheet)  //���� �׳� ����Ʈ ���̸� �˾Ƶ� �ɰ� ���⵵ �ϰ�
        {
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
        }
    }
}

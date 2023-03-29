using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootingSystem : MonoBehaviour
{
    List<GameObject> itemList;
    GameObject itemPrefab;

    private void Start()
    {
        //itemList = Util.GetORAddComponet<ItemData>(gameObject);
    }

    private void Update()
    {
        foreach (GameObject item in itemList)
        {
            Instantiate(item, transform.position, Quaternion.identity);
        }
        gameObject.SetActive(false);
        //루팅 종료? 
    }

    void Loot()
    {
        int clacDropChance = Random.Range(0, 1000);
        foreach(var item in itemList)
        {
            //if (item.dropChance < clacDropChance and item.type == ?)
        }
    }
}

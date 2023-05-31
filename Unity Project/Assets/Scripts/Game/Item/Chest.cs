using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Item item;
    private void Start()
    {
        int index = Random.Range(0, ItemList.Instance.tier1itemList.Count);
        item = ItemList.Instance.tier1itemList[index];

        OpenEvent();
    }

    public void OpenEvent()
    {
        GameObject obj = Instantiate(item.prefab);
        Rigidbody rb = Util.GetORAddComponet<Rigidbody>(obj);
        rb.AddForce(Vector3.up * 10);
    }

}

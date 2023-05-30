using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BundleofFireworks : Item
{
    public ItemData iteminfo;
    public GameObject misile;
    public Rigidbody rd;
    public float misileSpeed = 5.0f;

    private void Start()
    {
        NormalChest.Instance.ItemDropList[0].Add(gameObject);
        rd = Util.GetORAddComponet<Rigidbody>(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Plyaer")
        {
            Destroy(gameObject);

            GameManager.Instance.Player.AttackCall.RemoveListener(OnEvent);
            GameManager.Instance.Player.AttackCall.AddListener(OnEvent);

            iteminfo.amount++;
        }
    }

    public void OnEvent(GameObject target)
    {
        Vector3 parent = GameManager.Instance.Player.transform.position;
        GameObject obj = Instantiate(misile, parent, Quaternion.identity);
        ObjectEventHandle oeh = Util.GetORAddComponet<ObjectEventHandle>(obj);
        oeh.objects["Target"] = target;

        oeh.UpdateEvent.AddListener(MisileUpdateEvnet);
    }

    void MisileUpdateEvnet(ObjectEventHandle handle)
    {
        handle.transform.position = Vector3.MoveTowards(handle.transform.position ,handle.objects["Target"].transform.position, Time.deltaTime * misileSpeed);
    }

    public void DropEvent()
    {
        rd.AddForce(Vector3.up + Vector3.forward);
    }
    
}

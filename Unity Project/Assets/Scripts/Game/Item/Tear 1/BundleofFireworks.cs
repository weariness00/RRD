using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BundleofFireworks : Item
{
    public GameObject misile;
    public float misileSpeed = 5.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ++amount;

            GameManager.Instance.Player.AttackCall.RemoveListener(OnEvent);
            GameManager.Instance.Player.AttackCall.AddListener(OnEvent);

            Debug.Log("충돌");
            gameObject.SetActive(false);
        }
    }

    public void OnEvent(GameObject target)
    {
        Debug.Log("어택");
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
    
}

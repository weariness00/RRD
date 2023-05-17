using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Non Target
/// </summary>
public class FireBall : Skill
{
    FireProperty property;
    public GameObject ballObject;

    public override void OnSkill()
    {
        base.OnSkill();

        GameObject obj = Instantiate(ballObject, transform.position, transform.rotation);
        ObjectEventHandle objEventHandle = obj.AddComponent<ObjectEventHandle>();
        objEventHandle.UpdateEvent.AddListener(UpdateEvent);
        objEventHandle.OnCollisionEnterEvent.AddListener(OnCollisionEnterEvent);
    }

    public void UpdateEvent(ObjectEventHandle objectEventHandle)
    {
        objectEventHandle.transform.Translate(Vector3.forward * Time.deltaTime * status.speed.Cal());
    }

    public void OnCollisionEnterEvent(Collision collision, ObjectEventHandle objEventHandle)
    {
        if (collision.gameObject.tag == "Player") return;

        Managers.Damage.Attack(collision.gameObject, status.damage.Cal());

        Destroy(objEventHandle.gameObject);
    }
}

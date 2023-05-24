using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

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
        VisualEffect vfx = obj.GetComponentInChildren<VisualEffect>();
        objEventHandle.componets.Add("VisualEffect", vfx);

        objEventHandle.UpdateEvent.AddListener(UpdateEvent);
        objEventHandle.OnTriggerEnterEvent.AddListener(OnTriggerEnterEvent);

        Destroy(obj, 10f);
    }

    public void UpdateEvent(ObjectEventHandle objectEventHandle)
    {
        objectEventHandle.transform.Translate(Vector3.forward * Time.deltaTime * status.speed.Cal());
    }

    public void OnTriggerEnterEvent(Collider collider, ObjectEventHandle objEventHandle)
    {
        if (collider.gameObject.tag == "Player") return;
        
        Status parentStatus = gameObject.transform.parent.GetComponentInParent<Status>();

        if(collider.tag == "Monster")
            Managers.Damage.Attack(collider.GetComponentInParent<Monster>(), status.damage.Cal() + parentStatus.damage.Cal());
        VisualEffect vfx = objEventHandle.componets["VisualEffect"] as VisualEffect;
        vfx.SendEvent("Impact");

        Destroy(objEventHandle.gameObject, 1f);
        objEventHandle.enabled = false;
    }
}

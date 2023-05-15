using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// Non Target
/// </summary>
public class FireBall : Skill
{
    FireProperty property;
    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * status.speed.Cal());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            return;

        property = Util.GetORAddComponet<FireProperty>(gameObject);
        property.damage = status.damage.Cal();
        property.ApplyEffect(other.gameObject);
        property.ApplyDebuff(other.gameObject);
    }
}

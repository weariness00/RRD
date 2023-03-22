using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WindCutter : Skill
{
	WindProperty property;
    public int cutterCount = 1;

    public override void OnSkill()
    {
        if (!isOn)
            return;
        base.OnSkill();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            return;

        property = Util.GetORAddComponet<WindProperty>(gameObject);
        property.damage = status.damage;
        property.ApplyEffect(other.gameObject);
        property.ApplyDebuff(other.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProperty : ElementalProperty
{
    public override void ApplyEffect(GameObject target)
    {
        // 불 속성에 대한 구체적인 기능 구현
        particle.Play();
        StartCoroutine(ReleaseTime());

        DamageController damageController = target.GetComponent<DamageController>();
        damageController.resultDamage += damage;
    }

    public override void ReleaseEffect()
    {
        // 불 속성의 기본적인 기능 해제 구현
        particle.Stop();
    }
}
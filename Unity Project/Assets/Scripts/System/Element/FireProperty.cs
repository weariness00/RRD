using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProperty : ElementalProperty
{
    public float dotDamageDuration = 1f;
    public override void ApplyEffect(GameObject target)
    {
        // 불 속성에 대한 구체적인 기능 구현
        particle.Play();
        StartCoroutine(ReleaseTime());

        DamageController damageController = target.GetComponent<DamageController>();
        damageController.resultDamage += damage;
        StartCoroutine(DotDamage(damageController));
    }

    public override void ReleaseEffect()
    {
        // 불 속성의 기본적인 기능 해제 구현
        particle.Stop();
    }

    IEnumerator DotDamage(DamageController damageController)
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(dotDamageDuration);            
            damageController.resultDamage += damage;
        }
    }
}
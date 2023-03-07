using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProperty : ElementalProperty
{
    public float dotDamageDuration = 1f;
    public override void ApplyEffect(GameObject target)
    {
        // �� �Ӽ��� ���� ��ü���� ��� ����
        particle.Play();
        StartCoroutine(ReleaseTime());

        DamageController damageController = target.GetComponent<DamageController>();
        damageController.resultDamage += damage;
        StartCoroutine(DotDamage(damageController));
    }

    public override void ReleaseEffect()
    {
        // �� �Ӽ��� �⺻���� ��� ���� ����
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
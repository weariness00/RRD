using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProperty : ElementalProperty
{
    public override void ApplyEffect(GameObject target)
    {
        // �� �Ӽ��� ���� ��ü���� ��� ����
        particle.Play();
        StartCoroutine(ReleaseTime());

        DamageController damageController = target.GetComponent<DamageController>();
        damageController.resultDamage += damage;
    }

    public override void ReleaseEffect()
    {
        // �� �Ӽ��� �⺻���� ��� ���� ����
        particle.Stop();
    }
}
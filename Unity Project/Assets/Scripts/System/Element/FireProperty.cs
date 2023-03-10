using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 
/// </summary>
public class FireProperty : ElementalProperty
{
    public int dotDamage = 1;
    public float dotDamageInterval = 1f;    // ������ ����
    public float dotDamgeTime = 3f; // ��Ʈ ������ ���� �ð�

    public override void ApplyEffect(GameObject target)
    {
        // �� �Ӽ��� ���� ��ü���� ��� ����
        unityEvent?.Invoke();   // ��ƼŬ ���� ���� ȿ��
        StartCoroutine(ReleaseTime());

        Managers.Damage.Attack(target, damage);
        StartCoroutine(DotDamage(target));
    }

    public override void ReleaseEffect()
    {
        // �� �Ӽ��� �⺻���� ��� ���� ����

    }

    IEnumerator DotDamage(GameObject target)
    {
        for (int i = 0; i < dotDamgeTime; i++)
        {
            yield return new WaitForSeconds(dotDamageInterval);
            Managers.Damage.Attack(target, dotDamage);
        }
    }
}
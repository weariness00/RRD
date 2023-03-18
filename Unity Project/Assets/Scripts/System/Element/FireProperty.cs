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
    public float dotDamageInterval = 1f;    // 데미지 간격
    public float dotDamgeTime = 3f; // 도트 데미지 지속 시간

    public override void ApplyEffect(GameObject target)
    {
        // 불 속성에 대한 구체적인 기능 구현
        unityEvent?.Invoke();   // 파티클 사운드 등의 효과
        StartCoroutine(ReleaseTime());

        Managers.Damage.Attack(target, damage);
        StartCoroutine(DotDamage(target));
    }

    public override void ReleaseEffect()
    {
        // 불 속성의 기본적인 기능 해제 구현

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
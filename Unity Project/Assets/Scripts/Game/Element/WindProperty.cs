using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindProperty : ElementalProperty
{
    public override void ApplyDebuff(GameObject target)
    {
        Managers.Damage.Debuff(target,DebuffType.Wind);
    }

    public override void ApplyEffect(GameObject target)
    {
        unityEvent?.Invoke();   // 파티클 사운드 등의 효과

        Managers.Damage.Attack(target, damage);
    }

    public override void ReleaseEffect()
    {
    }
}

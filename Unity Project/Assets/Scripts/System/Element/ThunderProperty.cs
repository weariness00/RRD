using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 번개 속성
/// 
/// </summary>
public class ThunderProperty : ElementalProperty
{
    public override void ApplyDebuff(GameObject target)
    {
    }

    public override void ApplyEffect(GameObject _Object)
    {
        unityEvent?.Invoke();
        OnElectricShock(_Object);
    }

    public override void ReleaseEffect()
    {

    }

    /// <summary>
    /// 미완성
    /// </summary>
    public void OnElectricShock(GameObject _Object)
	{
        // 대상 object의 모든 애니메이션을 정지 후
        // 대상 Object의 Animation을 감전 Animaition으로 바꾸기
        Managers.Damage.Attack(_Object, damage);
	}


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 번개 속성
/// 
/// </summary>
public class ThunderProperty : ElementalProperty
{
    public int percentage;

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
        // 애니메이션 정지는 물속성(빙결)
        // 대상 Object의 Animation을 감전 Animaition으로 바꾸기

        // 감전은 몬스터가 받는 피해 증가
        /*if ( == MonsterStatus.Lighting)
            Managers.Damage.Attack(_Object, damage * percentage);*/

        _Object.GetComponent<MonsterInfo>().status = MonsterStatus.Lighting;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� �Ӽ�
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
    /// �̿ϼ�
    /// </summary>
    public void OnElectricShock(GameObject _Object)
	{
        // ��� object�� ��� �ִϸ��̼��� ���� ��
        // ��� Object�� Animation�� ���� Animaition���� �ٲٱ�
        Managers.Damage.Attack(_Object, damage);
	}


}

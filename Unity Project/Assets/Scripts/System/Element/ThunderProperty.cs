using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� �Ӽ�
/// 
/// </summary>
public class ThunderProperty : ElementalProperty
{
    public int percentage;
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
        // �ִϸ��̼� ������ ���Ӽ�(����)
        // ��� Object�� Animation�� ���� Animaition���� �ٲٱ�

        // ������ ���Ͱ� �޴� ���� ����
        /*if ( == MonsterStatus.Lighting)
            Managers.Damage.Attack(_Object, damage * percentage);*/

        _Object.GetComponent<MonsterInfo>().status = MonsterStatus.Lighting;
	}


}

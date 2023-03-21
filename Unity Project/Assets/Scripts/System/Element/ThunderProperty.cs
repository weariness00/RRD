using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� �Ӽ�
/// 
/// </summary>
public class ThunderProperty : ElementalProperty
{
    MonsterInfo monsterStatus;
    public int percentage;

    private void Start()
    {
        monsterStatus = GetComponent<MonsterInfo>();
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
        if (monsterStatus.status == MonsterStatus.Lighting)
            Managers.Damage.Attack(_Object, damage * percentage);
	}
}

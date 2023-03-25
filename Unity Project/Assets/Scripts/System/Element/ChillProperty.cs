using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChillProperty : ElementalProperty
{

    private void Start()
    {
    }

    public override void ApplyEffect(GameObject _Object)
    {
        unityEvent?.Invoke();
        StartCoroutine("ChillDuration");
    }

    public override void ReleaseEffect()
    {

    }

    IEnumerable ChillDuration(GameObject _Object)
    {
        // �÷��̾��� ���� �ӵ� ����
        _Object.GetComponent<PlayerController>().motionSpeed = new Vector3(0.1f, 0.1f, 0.1f);  //�������� ���� ������ �ʿ��� �� ������
        yield return new WaitForSeconds(durationTime);
        // ����
    }

    public override void ApplyDebuff(GameObject target)
    {
        
    }
}
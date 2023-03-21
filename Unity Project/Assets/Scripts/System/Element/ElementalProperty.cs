using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public enum DebuffType
{
    Fire,
    Thender,
    Wind,
    Water,
    Rock,
}

/// <summary>
/// �Ӽ����� �������� �ֻ��� Ŭ����
/// </summary>
public abstract class ElementalProperty : MonoBehaviour
{
    public string propertyName;
    public float releaseTime = 1f;
    [Space]

    public int damage;
    public bool isCrowdController;
    [Space]

    public UnityEvent unityEvent;
    public Image image;

    // ������� ������ �Լ�
    public abstract void ApplyDebuff(GameObject target);

    // �ش� �Ӽ��� �⺻���� ����� ������ �Լ�
    public abstract void ApplyEffect(GameObject target);

    // �ش� �Ӽ��� �⺻���� ����� �����ϴ� �Լ�
    public abstract void ReleaseEffect();

    public IEnumerator ReleaseTime()
    {
        yield return new WaitForSeconds(releaseTime);
        ReleaseEffect();
    }
}
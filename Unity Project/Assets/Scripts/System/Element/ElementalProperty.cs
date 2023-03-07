using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �Ӽ����� �������� �ֻ��� Ŭ����
/// </summary>

public class ElementalProperty : MonoBehaviour
{
    public string propertyName;
    public float releaseTime = 1f;
    [Space]

    public ParticleSystem particle;
    public Image image;
    [Space]

    public int damage;

    // �ش� �Ӽ��� �⺻���� ����� ������ �Լ�
    public abstract void ApplyEffect(GameObject obj);

    // �ش� �Ӽ��� �⺻���� ����� �����ϴ� �Լ�
    public abstract void ReleaseEffect();

    public IEnumerator ReleaseTime()
    {
        yield return new WaitForSeconds(releaseTime);
        ReleaseEffect();
    }
}
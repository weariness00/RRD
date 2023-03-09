using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// �Ӽ����� �������� �ֻ��� Ŭ����
/// </summary>

public abstract class ElementalProperty : MonoBehaviour
{
    public string propertyName;
    public float releaseTime = 1f;
    [Space]

    public int damage;
    public float speed = 1f;
    [Space]

    public UnityEvent unityEvent;
    public Image image;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

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
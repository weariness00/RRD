using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 속성들이 가져야할 최상위 클래스
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

    // 해당 속성의 기본적인 기능을 구현한 함수
    public abstract void ApplyEffect(GameObject obj);

    // 해당 속성의 기본적인 기능을 해제하는 함수
    public abstract void ReleaseEffect();

    public IEnumerator ReleaseTime()
    {
        yield return new WaitForSeconds(releaseTime);
        ReleaseEffect();
    }
}
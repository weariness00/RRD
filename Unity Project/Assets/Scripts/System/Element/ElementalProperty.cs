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
/// 속성들이 가져야할 최상위 클래스
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

    // 디버프를 구현할 함수
    public abstract void ApplyDebuff(GameObject target);

    // 해당 속성의 기본적인 기능을 구현한 함수
    public abstract void ApplyEffect(GameObject target);

    // 해당 속성의 기본적인 기능을 해제하는 함수
    public abstract void ReleaseEffect();

    public IEnumerator ReleaseTime()
    {
        yield return new WaitForSeconds(releaseTime);
        ReleaseEffect();
    }
}
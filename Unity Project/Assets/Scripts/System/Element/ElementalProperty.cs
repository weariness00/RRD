using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 속성들이 가져야할 최상위 클래스
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
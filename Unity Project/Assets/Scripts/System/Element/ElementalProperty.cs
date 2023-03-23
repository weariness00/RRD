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

public abstract class ElementalProperty : MonoBehaviour
{
    public string propertyName;
    public float releaseTime = 1f;
    [Space]

    public int damage;
    public float durationTime;
    public float speed = 1f;
    public bool isCrowdController;
    [Space]

    public UnityEvent unityEvent;
    public Image image;

    public abstract void ApplyDebuff(GameObject target);

    public abstract void ApplyEffect(GameObject target);

    public abstract void ReleaseEffect();

    public IEnumerator ReleaseTime()
    {
        yield return new WaitForSeconds(releaseTime);
        ReleaseEffect();
    }
}
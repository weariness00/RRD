using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public float hp;
    public float maxHp;
    public float mp;
    public float maxMp;
    [Space]

    public float damage;
    public int strike;
    Dictionary<DebuffType, Action> propertyDictionary;
    [Space]

    public float speed;
    public float range;

    private void Start()
    {
        hp = maxHp; mp = maxMp;
        range = 1f;
        speed = 1f;
    }
}

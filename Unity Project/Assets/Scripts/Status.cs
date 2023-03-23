using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public int hp;
    public int maxHp;
    public int mp;
    public int maxMp;
    [Space]

    public int damage;
    public int strike;
    Dictionary<DebuffType, Action> propertyDictionary;
    [Space]

    public float speed;
    public float range;

    private void Start()
    {
        hp = maxHp; mp = maxMp;
        range = 1;
        speed = 1;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public int level = 1;
    public float experience = 0f;
    [Space]

    public float hp = 10f;
    public float recoveryHP = 1f;
    public float maxHp;
    public float mp = 1f;
    public float recoveryMP = 1f;
    public float maxMp;
    [Space]

    public float damage;
    public float criticalProbability;   // 치명타 확률
    public float criticalDamagePower;   // 치명태 배율
    Dictionary<DebuffType, Action> propertyDictionary;
    [Space]

    public float speed;
    public float range;
    [Space]

    public bool dead;

    private void Start()
    {
        hp = maxHp; mp = maxMp;
        range = 1f;
        speed = 1f;

        StartCoroutine(Recovery());
    }

    IEnumerator Recovery()
    {
        while(true)
        {
            yield return stdfx.OneSecond;
            if (hp < maxHp) hp += recoveryHP;
            if (mp < maxMp) mp += recoveryMP;
        }
    }

    public bool LevelUP()
    {
        float needExp = level * 35f;
        if(needExp < experience)
        {
            level++;
            experience -= needExp;

            return true;
        }

        return false;
    }
    }

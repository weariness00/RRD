using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StatusValue
{
    public float value;     //증가(깡스탯)
    public float percent;       //증폭

    public float Cal()
    {
        return value * (1.0f + percent);
    }

    public bool ItemChance()
    {
        float chance = UnityEngine.Random.Range(0.0f, 100.0f);
        if (value <= chance)
            return true;

        return false;
    }
}

public class Status : MonoBehaviour
{
    public int level = 1;
    public float experience;
    public float experiencePercent;     //얘는 몬스터가 죽을 때 더해지는 값에 곱해야함
    [Space]

    public StatusValue hp;
    public StatusValue maxHp;
    public StatusValue mp;
    public StatusValue maxMp;
    public StatusValue BlockChance;
    [Space]

    public StatusValue damage;
    public StatusValue attackSpeed;
    public StatusValue criticalProbability;   // 치명타 확률 -> 이거 0~100으로 
    public StatusValue criticalDamagePower;   // 치명태 배율       
    Dictionary<DebuffType, Action> propertyDictionary;
    [Space]

    public StatusValue speed;
    public StatusValue range;
    public int jumpCount;
    [Space]

    public bool dead;

    private void Start()
    {
        maxHp.value = 100f;
        maxMp.value = 100f;
        hp = maxHp; mp = maxMp;
        range.value = 1f;
        speed.value = 1f;
        jumpCount = 1;
    }

    public bool LevelUP()
    {
        float needExp = level * 35f;
        if (needExp < experience)
        {
            level++;
            experience -= needExp;

            return true;
        }

        return false;
    }
}

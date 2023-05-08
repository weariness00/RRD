using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StatusValue
{
    public float value;     //����(������)
    public float percent;       //����

    public float Cal()
    {
        return value * (1.0f + percent);
    }
}

public class Status : MonoBehaviour
{
    public int level = 1;
    public float experience;
    public float experiencePercent;     //��� ���Ͱ� ���� �� �������� ���� ���ؾ���
    [Space]

    public StatusValue hp;
    public StatusValue maxHp;
    public StatusValue mp;
    public StatusValue maxMp;
    [Space]

    public StatusValue damage;
    public StatusValue attackSpeed;
    public StatusValue criticalProbability;   // ġ��Ÿ Ȯ��
    public StatusValue criticalDamagePower;   // ġ���� ����       
    Dictionary<DebuffType, Action> propertyDictionary;
    [Space]

    public StatusValue speed;
    public StatusValue range;
    [Space]

    public bool dead;

    private void Start()
    {
        maxHp.value = 100f;
        maxMp.value = 100f;
        hp = maxHp; mp = maxMp;
        range.value = 1f;
        speed.value = 1f;
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

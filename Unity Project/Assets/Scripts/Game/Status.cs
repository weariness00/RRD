using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StatusValue
{
    public float value;   
    public float percent;  

    public float Cal()
    {
        return value * (1.0f + percent);
    }
}

[System.Serializable]
public class StatusData
{
    public int ID;
    public string Name;
    public float MaxHP;
    public float HP;
    public float MaxMP;
    public float MP;
    public float Shield;
    public float Damage;
    public float AttackSpeed;
    public float Speed;
    public float Range;
}

public class Status : MonoBehaviour
{
    public int level = 1;
    public float need_Exp = 1 * 35f;
    public float experience;
    public float experiencePercent;   
    [Space]

    public StatusValue hp;
    public StatusValue maxHp;
    public StatusValue mp;
    public StatusValue maxMp;
    public StatusValue shield;
    public StatusValue BlockChance;
    [Space]

    public StatusValue damage;
    public StatusValue attackSpeed;
    public StatusValue criticalProbability;  
    public StatusValue criticalDamagePower;      
    Dictionary<DebuffType, Action> propertyDictionary;
    [Space]

    public StatusValue speed;
    public StatusValue range;
    public int jumpCount;
    [Space]

    public bool isDead;

    private void Start()
    {
        StartCoroutine(Recovery());
    }

    IEnumerator Recovery()
    {
        while(true)
        {
            yield return stdfx.OneSecond;

            if (hp.Cal() <= 0)
            {
                StopCoroutine(Recovery());
                break;
            }

            Mathf.Clamp(hp.value,0, maxHp.Cal());
            Mathf.Clamp(mp.value,0, maxMp.Cal());
        }
    }

    public bool LevelUP()
    {
        if (need_Exp < experience)
        {
            experience -= need_Exp;
            need_Exp = ++level * 35f;

            return true;
        }

        return false;
    }

    public void SetData(StatusData data)
    {
        maxHp.value = data.MaxHP;
        maxMp.value = data.MaxMP;
        hp.value = data.HP;
        mp.value = data.MP;
        shield.value = data.Shield;

        damage.value = data.Damage;
        attackSpeed.value = data.AttackSpeed;
        speed.value = data.Speed;

        range.value = data.Range;
    }
}
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
    public float experiencePercent;   
    [Space]

    public StatusValue hp;
    public StatusValue maxHp;
    public StatusValue mp;
    public StatusValue maxMp;
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

    public bool dead;

    private void Start()
    {
        StartCoroutine(Recovery());
    }

    IEnumerator Recovery()
    {
        while(true)
        {
            yield return stdfx.OneSecond;
            Mathf.Clamp(hp.value,0, maxHp.Cal());
            Mathf.Clamp(mp.value,0, maxMp.Cal());
        }
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
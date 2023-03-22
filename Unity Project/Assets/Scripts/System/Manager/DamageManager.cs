using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 데미지 계산 관련 클래스
/// </summary>
public class DamageManager
{
    Dictionary<GameObject, DamageInfo> resultDamageDictionary = new Dictionary<GameObject, DamageInfo>();

    public void LateUpdate()
    {
        foreach (var resultDamage in resultDamageDictionary)
        {
            Status info = resultDamage.Key.GetComponent<Status>();
            if(!info)
            {
                Debug.Log($"Not Have Status : {resultDamage.Key.name}");
                continue;
            }

            info.hp -= resultDamage.Value.damage;
            Debug.Log($"\"{info.name}\" Under Attack ( Damage : {resultDamage.Value.damage} )");
        }

        resultDamageDictionary.Clear();
    }

    void AddObject(GameObject obj)
    {
        if (resultDamageDictionary.ContainsKey(obj))
            return;

        DamageInfo info = new DamageInfo();

        resultDamageDictionary.Add(obj, info);
    }

    public void Attack(GameObject obj, int _Damage)
    {
        AddObject(obj);

        //몬스터가 감전 상태일 경우 받는 피해 증폭
        if (obj.GetComponent<MonsterStatus>() == MonsterStatus.Lighting)
            //resultDamageDictionary[obj].damage += _Damage * 1.15f;  // 나중에 수치 조절 필요
        else
            resultDamageDictionary[obj].damage += _Damage;
    }

    public void Attack(GameObject obj, Status status) { Attack(obj, status.damage); }
}

class DamageInfo
{
    public int damage;
    public int heal;
    public bool[] isProperty = new bool[5]; // 1.불 2.물 3.흙 4.바람 5.번개

    public DamageInfo()
    {
        damage = 0;
        heal = 0;
        Array.Clear(isProperty, 0, 4);
    }
}
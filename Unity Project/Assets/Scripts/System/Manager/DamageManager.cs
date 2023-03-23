using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 데미지 계산 관련 클래스
/// </summary>
public class DamageManager
{
    Dictionary<GameObject, DamageInfo> resultDamageDictionary = new Dictionary<GameObject, DamageInfo>();

    public DamageManager()
    {
        Managers.Instance.LateUpdateCall += LateUpdate;
    }

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

            CheckDebuff(info, resultDamage.Value);

            info.hp -= resultDamage.Value.damage;
            Debug.Log($"\"{info.name}\" Under Attack ( Damage : {resultDamage.Value.damage} )");
        }

        resultDamageDictionary.Clear();
    }

    DamageInfo AddObject(GameObject obj)
    {
        if (resultDamageDictionary.ContainsKey(obj))
            return resultDamageDictionary[obj];

        DamageInfo info = new DamageInfo();

        resultDamageDictionary.Add(obj, info);
        return info;
    }

    public void Attack(GameObject obj, float _Damage)
    {
        AddObject(obj);

        //몬스터가 감전 상태일 경우 받는 피해 증폭
        if (obj.GetComponent<MonsterStatus>() == MonsterStatus.Lighting)
            resultDamageDictionary[obj].damage += _Damage * 1.15f;  // 나중에 수치 조절 필요
        else
            resultDamageDictionary[obj].damage += _Damage;
    }

    public void Attack(GameObject obj, Status status) { Attack(obj, status.damage); }

    public void Debuff(GameObject obj, DebuffType debuffType)
    {
        DamageInfo dInfo = AddObject(obj);

        if (!dInfo.debuffDictionary.ContainsKey(debuffType))
            dInfo.debuffDictionary.Add(debuffType, 0);

        dInfo.debuffDictionary[debuffType]++;
    }

    // 어떤 디버프가 들어왔냐에 따른 검사.
    void CheckDebuff(Status status, DamageInfo dInfo)
    {

    }
}

class DamageInfo
{
    public float damage;
    public float heal;
    public Dictionary<DebuffType, int> debuffDictionary = new Dictionary<DebuffType, int>();    // 임시 // 디버프가 몇 중첩인지

    public DamageInfo()
    {
        damage = 0f;
        heal = 0f;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ��� ���� Ŭ����
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
            GameObject obj = resultDamage.Key;
            DamageInfo info = resultDamage.Value;
            Status status = obj.GetComponent<Status>();
            if(!status)
            {
                Debug.Log($"Not Have Status : {obj.name}");
                continue;
            }

            CheckDebuff(status, info);

            info.iDamage?.Hit(info.damage);
            Debug.Log($"\"{status.name}\" Under Attack ( Damage : {info.damage} )");
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

        //���Ͱ� ���� ������ ��� �޴� ���� ����
        //if (obj.GetComponent<MonsterStatus>() == MonsterStatus.Lighting)
        //    resultDamageDictionary[obj].damage += _Damage * 1.15f;  // ���߿� ��ġ ���� �ʿ�
        //else
        //    resultDamageDictionary[obj].damage += _Damage;

        resultDamageDictionary[obj].damage += _Damage;
    }

    public void Attack<T>(T obj, float _Damage) where T : UnityEngine.Component
    {
        AddObject(obj.gameObject);
        DamageInfo info = resultDamageDictionary[obj.gameObject];
        info.iDamage = obj as IDamage;
        info.damage += _Damage;
    }

    public void Attack(GameObject obj, Status status) { Attack(obj, status.damage); }

    public void Debuff(GameObject obj, DebuffType debuffType)
    {
        DamageInfo dInfo = AddObject(obj);

        if (!dInfo.debuffDictionary.ContainsKey(debuffType))
            dInfo.debuffDictionary.Add(debuffType, 0);

        dInfo.debuffDictionary[debuffType]++;
    }

    // � ������� ���ԳĿ� ���� �˻�.
    void CheckDebuff(Status status, DamageInfo dInfo)
    {

    }
}

class DamageInfo
{
    public IDamage iDamage;

    public float damage;
    public float heal;
    public Dictionary<DebuffType, int> debuffDictionary = new Dictionary<DebuffType, int>();    // �ӽ� // ������� �� ��ø����

    public DamageInfo()
    {
        damage = 0f;
        heal = 0f;
    }
}

public interface IDamage
{
    public void Hit(float damage);
}
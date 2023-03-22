using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ������ ��� ���� Ŭ����
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

        //���Ͱ� ���� ������ ��� �޴� ���� ����
        if (obj.GetComponent<MonsterStatus>() == MonsterStatus.Lighting)
            //resultDamageDictionary[obj].damage += _Damage * 1.15f;  // ���߿� ��ġ ���� �ʿ�
        else
            resultDamageDictionary[obj].damage += _Damage;
    }

    public void Attack(GameObject obj, Status status) { Attack(obj, status.damage); }
}

class DamageInfo
{
    public int damage;
    public int heal;
    public bool[] isProperty = new bool[5]; // 1.�� 2.�� 3.�� 4.�ٶ� 5.����

    public DamageInfo()
    {
        damage = 0;
        heal = 0;
        Array.Clear(isProperty, 0, 4);
    }
}
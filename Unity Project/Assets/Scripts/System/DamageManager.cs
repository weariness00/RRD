using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ������ ��� ���� Ŭ����
/// </summary>
public class DamageManager : MonoBehaviour
{
    Dictionary<GameObject, DamageInfo> resultDamageDictionary = new Dictionary<GameObject, DamageInfo>();

    private void LateUpdate()
    {
        foreach (var resultDamage in resultDamageDictionary)
        {
            Status info = resultDamage.Key.GetComponent<Status>();
            info.hp -= resultDamage.Value.damage;
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

        resultDamageDictionary[obj].damage += _Damage;
    }
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
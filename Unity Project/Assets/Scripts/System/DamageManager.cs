using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 데미지 계산 관련 클래스
/// </summary>
public class DamageManager : MonoBehaviour
{
    Dictionary<GameObject, int> resultDamageDictionary;

    private void LateUpdate()
    {
        foreach (var resultDamage in resultDamageDictionary)
        {
            Status info = resultDamage.Key.GetComponent<Status>();
            info.hp -= resultDamage.Value;
        }

        resultDamageDictionary.Clear();
    }

    void AddObject(GameObject obj)
    {
        if (resultDamageDictionary.ContainsKey(obj))
            return;
        
        resultDamageDictionary.Add(obj, 0);
    }

    public void Attack(GameObject obj, int _Damage)
    {
        AddObject(obj);

        resultDamageDictionary[obj] += _Damage;
    }
}
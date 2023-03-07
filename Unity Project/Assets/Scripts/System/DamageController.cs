using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 데미지 계산 관련 클래스
/// </summary>
public class DamageController : Subject
{
    Status info;
    [HideInInspector] public int resultDamage { get; set; }
    [HideInInspector] public int resultHeal { get; set; }

    private void Awake()
    {
        info = GetComponent<Status>();
    }

    private void LateUpdate()
    {
        info.hp += resultHeal - resultDamage;
        Debug.Log(info.hp);

        resultDamage = 0;
        resultHeal = 0;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static System.Net.WebRequestMethods;

[System.Serializable]
public enum MonsterType
{
    Ground,
    UnderGround,
    Fly
};

[System.Serializable]
public enum MonsterRate
{
    Normal,
    Elite,
    Boss
};

[System.Serializable]
public enum MonsterStatus
{
    Fire,
    Lighting,
    Water,
    Earth,
    Wind
};

[System.Serializable]
public class MonsterInfo
{
    public int id;
    public string name;

    [Space]
    public MonsterType type;
    public MonsterRate rate;
    public MonsterStatus status;

    [Space]
    public int hp;
    public int mp;

    [Space]
    public int damage;
    public float speed;
}

public class Monster : MonoBehaviour
{
    public int id;

    [Space]
    public MonsterType type;
    public MonsterRate rate;

    [HideInInspector] public Status status;
    public Animator animator;

    public FSMStructer<Monster> fsm;
    public FindToMove ftm;

    private void Awake()
    {
        status = Util.GetORAddComponet<Status>(gameObject);
    }

    private void Update()
    {
        if (status.hp <= 0)
            Dead();
    }

    // 받아온 데이터를 넣어준다.
    public void Init(MonsterInfo info)
    {
        ftm = Util.GetORAddComponet<FindToMove>(gameObject);

        status = Util.GetORAddComponet<Status>(gameObject);
        name = info.name;

        id = info.id;
        type = info.type;
        rate = info.rate;
        status.hp = info.hp;
        status.maxHp = info.hp;
        status.mp = info.mp;
        status.maxMp = info.mp;
        status.damage = info.damage;
    }

    public void Dead()
    {
        // 죽을때 애니메이션
        // 만약 필요하다면 파티클도
        // 아이템 루팅도 추가
        // 킬 카운트에 포함
        //MonsterSpawnManager.Instance.aliveMonsterCount--;
        // 다 끝난후 객체 소멸시키기
        Destroy(gameObject, 30f);
    }
}
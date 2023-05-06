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

    // �޾ƿ� �����͸� �־��ش�.
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
        // ������ �ִϸ��̼�
        // ���� �ʿ��ϴٸ� ��ƼŬ��
        // ������ ���õ� �߰�
        // ų ī��Ʈ�� ����
        //MonsterSpawnManager.Instance.aliveMonsterCount--;
        // �� ������ ��ü �Ҹ��Ű��
        Destroy(gameObject, 30f);
    }
}
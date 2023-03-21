using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float speed;
    public int damage;
    public int hp;
    public int mp;
}

public class Monster : MonoBehaviour
{
    public MonsterInfo info;

    public virtual Monster Clone()
    {
        return new Monster();
    }
}
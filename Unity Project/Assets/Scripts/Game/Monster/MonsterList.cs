using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ Monster Prefab�� �־� �δ� List�� ��ũ��Ʈ
/// ��ư�� ������ ������ ������ �°� �������ش�.
/// </summary>
public class MonsterList : MonoBehaviour
{
    public static MonsterList Instance = null;

    public MonsterData monsterData;
    public GameObject[] monsterList;

    private void Awake()
    {
        Instance = this;

        InitList();
    }

    public MonsterInfo GetMonsterData(int index) { return monsterData.data[index]; }
    public MonsterInfo GetMonsterData(string monsterName)
    {
        foreach (var data in monsterData.data)
        {
            if (data.name.Equals(monsterName))
                return data;
        }

        return null;
    }

    public GameObject RandomMonster()
    {
        return monsterList[Random.Range(0, monsterList.Length)];
    }

    public void InitList()
    {
        GameObject[] objs = Resources.LoadAll<GameObject>("Prefabs/Monster");
        monsterList = new GameObject[objs.Length];

        foreach (var obj in objs)
        {
            int id = monsterData.data.Find((info) => info.name.Equals(obj.name)).id;
            monsterList[id] = obj;
        }
    }
}

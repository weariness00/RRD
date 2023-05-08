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
    public List<GameObject> monsterList;

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
        return monsterList[Random.Range(0, monsterList.Count)];
    }

    public void InitList()
    {
        monsterList.Clear();

        GameObject[] objs = Resources.LoadAll<GameObject>("Prefabs/Monster");
        foreach (var obj in objs)
        {
            int id = monsterData.data.Find((info) => info.name.Equals(obj.name)).id;
            monsterList.Insert(id, obj);
        }
    }
}

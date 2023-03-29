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

    public void SortMonsterList()
    {
        int count = 0;
        while(true)
        {
            for (int i = 0; i < monsterData.data.Count; i++)
            {
                if (monsterData.data[count].name.Equals(monsterList[i].name))
                {
                    GameObject temp = monsterList[i];                
                    monsterList[i] = monsterList[count];
                    monsterList[count] = temp;

                    count++;
                    break;
                }
            }

            if (count.Equals(monsterData.data.Count))
                return;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 만들어둔 Monster Prefab을 넣어 두는 List용 스크립트
/// 버튼을 누르면 엑셀의 순서에 맞게 정렬해준다.
/// </summary>
public class MonsterList : MonoBehaviour
{
    public static MonsterList Instance = null;

    public MonsterDataExcel monsterDataExcel;
    public GameObject[] monsterList;

    private void Awake()
    {
        Instance = this;

        InitList();
    }

    public MonsterData GetMonsterData(int index) 
    {
        MonsterData data = new MonsterData();
        data.type = monsterDataExcel.Type[index];
        data.status = monsterDataExcel.Default_Status[index];
        return data;
    }
    public MonsterData GetMonsterData(string monsterName)
    {
        MonsterData data = new MonsterData();
        for (int i = 0; i < monsterDataExcel.Type.Count; i++)
        {
            if (monsterDataExcel.Type[i].name.Equals(monsterName))
            {
                data.type = monsterDataExcel.Type[i];
                data.status = monsterDataExcel.Default_Status[i];
                return data;
            }
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
            int id = monsterDataExcel.Type.Find((type) => type.name.Equals(obj.name)).id;
            monsterList[id] = obj;
        }
    }
}

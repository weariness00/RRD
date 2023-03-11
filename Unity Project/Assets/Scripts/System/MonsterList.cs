using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterList : MonoBehaviour
{
    public static MonsterList instance = null;

    public MonsterData monsterData;
    public List<GameObject> monsterList;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public static MonsterList Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
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

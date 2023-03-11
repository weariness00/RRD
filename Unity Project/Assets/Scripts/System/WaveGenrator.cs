using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[System.Serializable]
public struct MonsterNode
{
    public GameObject monster;
    public int spawnCount;
    public float spawnTime;
}

[System.Serializable]
public class WaveNode
{
    public List<MonsterNode> waveMonsterList;
    public string award;

    public WaveNode()
    {
        waveMonsterList = new List<MonsterNode>();
    }
}

public class WaveGenrator : MonoBehaviour
{
    public GameObject[] buttons;

    public List<WaveNode> waveList;
    WaveNode currentWaveNode;

    // 몬스터 List 에서 랜덤한 몬스터를 가져와 3개의 wave를 만들기
    // 만들어진 웨이브를 버튼에 텍스쳐로 표시
    // 선택된 웨이브의 정보를 MonsterSpawnManager에 넘기기

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        for (int i = 0; i < 3; i++)
        {
            TMP_Text text = buttons[i].GetComponentInChildren<TMP_Text>();
            text.text = "";
            MakeWaveNode(text);
        }
    }

    public void MakeWaveNode(TMP_Text text)
    {
        WaveNode waveNode = new WaveNode();
        for (int i = 0; i < 3; i++)
        {
            MonsterNode monsterNode = new MonsterNode();
            monsterNode.monster = MonsterList.instance.RandomMonster();
            monsterNode.spawnCount = Random.Range(5, 10);
            monsterNode.spawnTime = Random.Range(8f, 12f);

            text.text += "Name : " + monsterNode.monster.name + "\n";
            text.text += "SpawnCount : " + monsterNode.spawnCount + "\n";
            text.text += "SpawnTime : " + monsterNode.spawnTime + "\n";        

            waveNode.waveMonsterList.Add(monsterNode);
        }

        waveList.Add(waveNode);
    }

    public void SelcetWave(int index)
    {
        if (index > waveList.Count)
        {
            print("범위를 넘어선 Index");
            return;
        }

        currentWaveNode = waveList[index - 1];
        MonsterSpawnManager.instance.waveNode = currentWaveNode;
        gameObject.SetActive(false);
    }
}

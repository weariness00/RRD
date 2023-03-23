using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine.UI;

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

    // ���� List ���� ������ ���͸� ������ 3���� wave�� �����
    // ������� ���̺긦 ��ư�� �ؽ��ķ� ǥ��
    // ���õ� ���̺��� ������ MonsterSpawnManager�� �ѱ��
    private void Start()
    {
        GameManager.Instance.StartWaveCall.AddListener(()=> { waveList.Clear(); gameObject.SetActive(false); });
        WaveGenerate();
    }

    public void WaveGenerate()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int temp = i;
            buttons[i].GetComponent<Button>().onClick.AddListener(() => { SelcetWave(temp); });
            TMP_Text text = buttons[i].GetComponentInChildren<TMP_Text>();
            text.text = "";
            MakeWaveNode(text);
        }
    }

    public void MakeWaveNode(TMP_Text text)
    {
        WaveNode waveNode = new WaveNode();
        for (int i = 0; i < buttons.Length; i++)
        {
            MonsterNode monsterNode = new MonsterNode();
            monsterNode.monster = MonsterList.instance.RandomMonster();
            monsterNode.spawnCount = Random.Range(5, 10);
            monsterNode.spawnTime = Random.Range(8f, 12f);

            text.text += "Name : " + monsterNode.monster.name + "\n";
            text.text += "SpawnCount : " + monsterNode.spawnCount + "\n";
            //text.text += "SpawnTime : " + monsterNode.spawnTime + "\n";        

            waveNode.waveMonsterList.Add(monsterNode);
        }
        text.text += $"Reward : {waveNode.award}";

        waveList.Add(waveNode);
    }

    public void SelcetWave(int index)
    {
        if (index > waveList.Count)
        {
            print("������ �Ѿ Index");
            return;
        }

        currentWaveNode = waveList[index];
        MonsterSpawnManager.Instance.waveNode = currentWaveNode;
    }
}

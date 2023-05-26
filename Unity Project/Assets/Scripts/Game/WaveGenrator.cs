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
public struct WaveNode
{
    public List<MonsterNode> monsterNodeList;
    public string award;

    public void Init()
    {
        monsterNodeList = new List<MonsterNode>();
        award = "";
    }
}

public class WaveGenrator : MonoBehaviour
{
    public Button[] buttons;

    public List<WaveNode> waveList;
    WaveNode currentWaveNode;

    // 몬스터 List 에서 랜덤한 몬스터를 가져와 3개의 wave를 만들기
    // 만들어진 웨이브를 버튼에 텍스쳐로 표시
    // 선택된 웨이브의 정보를 MonsterSpawnManager에 넘기기
    private void Start()
    {
        GameManager.Instance.StartWaveCall.AddListener(()=> { waveList.Clear(); gameObject.SetActive(false); });
        GameManager.Instance.StopWaveCall.AddListener(WaveGenerate);

        GameManager.Instance.UpdateCall.AddListener(OnOff);

        WaveGenerate();

        gameObject.SetActive(false);
    }

    public void OnOff()
    {
        if (Managers.Key.InputActionDown(KeyToAction.WaveGenerator_UI))
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }

    public void WaveGenerate()
    {
        gameObject.SetActive(true);
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
        waveNode.Init();
        for (int i = 0; i < buttons.Length; i++)
        {
            MonsterNode monsterNode = new MonsterNode();
            monsterNode.monster = MonsterList.Instance.RandomMonster();
            monsterNode.spawnCount = Random.Range(5, 10);
            monsterNode.spawnTime = Random.Range(8f, 12f);

            text.text += "Name : " + monsterNode.monster.name + "\n";
            text.text += "SpawnCount : " + monsterNode.spawnCount + "\n";
            //text.text += "SpawnTime : " + monsterNode.spawnTime + "\n";        

            waveNode.monsterNodeList.Add(monsterNode);
        }
        text.text += $"Reward : {waveNode.award}";

        waveList.Add(waveNode);
    }

    public void SelcetWave(int index)
    {
        if (index > waveList.Count)
        {
            print("범위를 넘어선 Index");
            return;
        }

        currentWaveNode = waveList[index];
        MonsterSpawnManager.Instance.waveNode = currentWaveNode;
    }
}

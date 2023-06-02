using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine.UI;
using UnityEditor.UI;

[System.Serializable]
public struct MonsterNode
{
    public Monster monster;
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
    public ToggleGroup toggleGroup;
    public Toggle[] buttons;

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

    private void OnEnable()
    {
        ++GameManager.Instance.OnWindowIndex;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnDisable()
    {
        --GameManager.Instance.OnWindowIndex;
        if (GameManager.Instance.OnWindowIndex.Equals(0))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void OnOff()
    {
        if (Managers.Key.InputActionDown(KeyToAction.WaveGenerator_UI))
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }

    public void StartWaveButton()
    {
        if (!toggleGroup.AnyTogglesOn() ||
            GameManager.Instance.isWave) return;
        GameManager.Instance.StartWave();
    }

    public void WaveGenerate()
    {
        gameObject.SetActive(true);
        for (int i = 0; i < buttons.Length; i++)
        {
            int temp = i;
            buttons[i].GetComponent<Toggle>().onValueChanged.AddListener((isOn) => { if(isOn) SelcetWave(temp); });

            MakeWaveNode(buttons[i]);
        }
    }

    public void MakeWaveNode<T>(T component) where T : UnityEngine.Component
    {
        WaveNode waveNode = new WaveNode();
        waveNode.Init();
        for (int i = 0; i < buttons.Length; i++)
        {
            MonsterNode monsterNode = new MonsterNode();
            monsterNode.monster = MonsterList.Instance.RandomMonster().GetComponent<Monster>();
            monsterNode.spawnCount = Random.Range(5, 10);
            monsterNode.spawnTime = Random.Range(8f, 12f);

            //monster 정보를 가시화
            GameObject infoUI = component.GetComponentInChildren<GridLayoutGroup>().transform.GetChild(i).gameObject;
            TMP_Text explain = infoUI.GetComponentInChildren<TMP_Text>();
            Image icon = null;
            foreach (var image in infoUI.GetComponentsInChildren<Image>())
                if (image.name.Equals("Icon")) icon = image;

            explain.fontSizeMin = 10;
            explain.fontSizeMax = 28;
            explain.text = "";
            explain.text += monsterNode.monster.name + "\n";
            explain.text += $"{monsterNode.spawnTime}초당\n{monsterNode.spawnCount}마리 소환";
            icon.sprite = monsterNode.monster.icon;

            waveNode.monsterNodeList.Add(monsterNode);
        }
        //text.text += $"Reward : {waveNode.award}";

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

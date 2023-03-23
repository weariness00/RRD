using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    [HideInInspector]public static MonsterSpawnManager Instance;

    public WaveNode waveNode;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void Start()
    {
        GameManager.Instance.StartWaveCall.AddListener(Spawn);
        GameManager.Instance.StopWaveCall.AddListener(StopSpawn);
    }

    void Spawn()
    {
        foreach(var monsterNode in waveNode.waveMonsterList)
        {
            StartCoroutine(monsterNodeSpawn(monsterNode));
        }
    }

    void StopSpawn()
    {
        foreach (var monsterNode in waveNode.waveMonsterList)
        {
            StopCoroutine(monsterNodeSpawn(monsterNode));
        }
    }

    IEnumerator monsterNodeSpawn(MonsterNode node)
    {
        WaitForSeconds waitSpawn = new WaitForSeconds(node.spawnTime);
        while (true)
        {
            yield return waitSpawn;
            GameObject obj = Util.Instantiate(node.monster);
            Monster monster = Util.GetORAddComponet<Monster>(obj);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    [HideInInspector]public static MonsterSpawnManager Instance;

    public GameObject spawnParant;  // 스폰이 될 지점에 대한 객체
    public float spawnDistance = 1f; // 현재 대상과의 얼마만큼의 거리에서 Spawn 될 것인지
    public int maxAliveMonsterCount = 100;
    int aliveMonsterCount = 0;

    public WaveNode waveNode;
    //public Vector3[] spawnSpot;
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
        foreach(var monsterNode in waveNode.monsterNodeList)
        {
            spawnCoroutineList.Add(monsterNodeSpawn(monsterNode));
        }

        foreach (var coroutine in spawnCoroutineList)
        {
            StartCoroutine(coroutine);
        }
    }

    void StopSpawn()
    {
        foreach (var coroutine in spawnCoroutineList)
        {
            StopCoroutine(coroutine);
        }
        spawnCoroutineList.Clear();
    }

    List<IEnumerator> spawnCoroutineList = new List<IEnumerator>();
    IEnumerator monsterNodeSpawn(MonsterNode node)
    {
        WaitForSeconds waitSpawn = new WaitForSeconds(node.spawnTime);
        while (true)    
        {
            if (aliveMonsterCount >= maxAliveMonsterCount)
            {
                yield return waitSpawn;
                continue;
            }

            GameObject monsterUnion = new GameObject { name = node.monster.name };
            monsterUnion.transform.parent = spawnParant.transform;

            for (int i = 0; i < node.spawnCount; i++)
            {             
                GameObject obj = Util.Instantiate(node.monster, monsterUnion.transform);
                Monster monster = Util.GetORAddComponet<Monster>(obj);
                monster.Init(MonsterList.Instance.GetMonsterData(node.monster.name));

                float radian = Mathf.Deg2Rad * UnityEngine.Random.Range(-180, 180);
                obj.transform.position = new Vector3(spawnDistance * Mathf.Sin(radian), 0, spawnDistance * Mathf.Cos(radian));

                aliveMonsterCount++;
            }
            yield return waitSpawn;
        }
    }
}
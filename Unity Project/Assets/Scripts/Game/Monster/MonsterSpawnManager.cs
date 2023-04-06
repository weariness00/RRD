using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    [HideInInspector]public static MonsterSpawnManager Instance;

    public GameObject spawnParant;  // 스폰이 될 지점에 대한 객체
    public float spawnDistance = 1f; // 현재 대상과의 얼마만큼의 거리에서 Spawn 될 것인지
    public int aliveMonsterCount = 0;
    [SerializeField] int maxAliveMonsterCount = 100;

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
        GameManager.Instance.StopWaveCall.AddListener(AliveMonsterAllKill);

        GameManager.Instance.UpdateCall.AddListener(OnOff);

        gameObject.SetActive(false);
    }

    public void OnOff()
    {
        if (Managers.Key.InputActionDown(KeyToAction.MonsterSpawnManager))
            gameObject.SetActive(!gameObject.activeSelf);
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
                ItemDropTable idt = Util.GetORAddComponet<ItemDropTable>(obj);

                monster.Init(MonsterList.Instance.GetMonsterData(node.monster.name));
                idt.SetDropItem(LootingSystem.Instance.SetDropTable(monster));
         
                float radian = Mathf.Deg2Rad * UnityEngine.Random.Range(-180, 180);
                obj.transform.position = new Vector3(spawnDistance * Mathf.Sin(radian), 0, spawnDistance * Mathf.Cos(radian));

                aliveMonsterCount++;
            }
            yield return waitSpawn;
        }
    }
    
    // 살아 있는 모든 몬스터 죽이기
    void AliveMonsterAllKill()
    {
        foreach (var child in Util.GetChildren(spawnParant))
        {
            foreach (var monster in Util.GetChildren<Monster>(child))
            {
                monster.Dead();
            }

            Destroy(child);
        }
    }
}
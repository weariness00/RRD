using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum MonsterSpawnType
{
    Distance,
    Spot,
}

[System.Serializable]
public struct BoolSpawnVectorPosition
{
    public bool x, y, z;
}


public class MonsterSpawnManager : MonoBehaviour
{
    [HideInInspector]public static MonsterSpawnManager Instance;
    public MonsterSpawnType type;
    [SerializeField] int maxAliveMonsterCount = 100;
    [SerializeField] BoolSpawnVectorPosition SpawnPositionType;

    [SerializeField] GameObject spawnPoolParant;  // 스폰이 될 지점에 대한 객체
    public float spawnMinDistance = 1f; // 현재 대상과의 얼마만큼의 거리에서 Spawn 될 것인지
    public float spawnMaxDistance = 1f; // 현재 대상과의 얼마만큼의 거리에서 Spawn 될 것인지
    public int aliveMonsterCount = 0;
    [SerializeField] bool isTarget;
    [SerializeField] Vector3 spawnPosition;
    [SerializeField] GameObject spawnTarget;
    [SerializeField] List<GameObject> spawnSpot_Object;

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
        GameManager.Instance.StopWaveCall.AddListener(AliveMonsterAllKill);
    }

    void Spawn()
    {
        foreach (var monsterNode in waveNode.monsterNodeList)
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
            monsterUnion.transform.parent = spawnPoolParant.transform;

            for (int i = 0; i < node.spawnCount; i++)
            {             
                GameObject obj = Util.Instantiate(node.monster.gameObject, monsterUnion.transform);
                Monster monster = Util.GetORAddComponet<Monster>(obj);
                ItemDropTable idt = Util.GetORAddComponet<ItemDropTable>(obj);

                monster.Init(MonsterList.Instance.GetMonsterData(node.monster.name));
                idt.SetDropItem(LootingSystem.Instance.SetDropTable(monster));

                float dis = Mathf.Clamp(0.0f, spawnMinDistance, spawnMaxDistance);
                Vector3 pos = Random.onUnitSphere * dis;
                switch (type)
                {
                    case MonsterSpawnType.Distance:
                        if (isTarget) pos += spawnTarget.transform.position;
                        else pos += spawnPosition;
                        break;
                    case MonsterSpawnType.Spot:
                        if(spawnSpot_Object.Count != 0)
                            pos += spawnSpot_Object[Random.Range(0, spawnSpot_Object.Count)].transform.position;
                        break;
                }
                obj.transform.position = new Vector3(SpawnPositionType.x ? pos.x : 0, SpawnPositionType.y ? pos.y : 0, SpawnPositionType.z ? pos.z : 0);

                aliveMonsterCount++;
            }
            yield return waitSpawn;
        }
    }
    
    // 살아 있는 모든 몬스터 죽이기
    void AliveMonsterAllKill()
    {
        foreach (var child in Util.GetChildren(spawnPoolParant))
        {
            foreach (var monster in Util.GetChildren<Monster>(child))
            {
                monster.Dead(0.0f);
            }
            Destroy(child);
        }
    }
}
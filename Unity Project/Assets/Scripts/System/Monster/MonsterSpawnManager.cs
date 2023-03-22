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

    private void Update()
    {
        if (!GameManager.Instance.isWaveStart)
            return;

        // 몬스터 소환 매커니즘
    }
}
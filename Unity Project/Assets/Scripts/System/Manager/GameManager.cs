using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector] public GameObject player;

    public bool isWaveStart = false;    // 현재 웨이브가 시작되었는지
    public float endTime = 60f; // 웨이브를 버텨야 하는 시간
    [Space]

    public UnityEvent StartWaveCall;

    private void Awake()
    {
        Instance = this;
        endWaveTime = new WaitForSeconds(endTime);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void StartWave()
    {
        isWaveStart = true;
        StartCoroutine(EndWaveTime());
        StartWaveCall?.Invoke();
    }

    public void EndWave()
    {
        
    }

    public void GameOver()
    {
        // 게입 오버 씬으로 전환
    }

    WaitForSeconds endWaveTime;
    IEnumerator EndWaveTime()
    {
        yield return new WaitForSeconds(endTime);
        isWaveStart = false;
        EndWave();
    }
}

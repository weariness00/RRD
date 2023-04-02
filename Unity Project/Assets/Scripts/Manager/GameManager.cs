using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
	public static GameManager Instance { get { Init(); return instance; } }

    public PlayerController Player;

    public bool isWave = false;
    public float waveTime = 60f;

    [HideInInspector] public UnityEvent SetDataCall;
    [HideInInspector] public UnityEvent StartWaveCall;
    [HideInInspector] public UnityEvent StopWaveCall;

    public int alivePlayerCount = 0;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        waitWaveTime = new WaitForSeconds(waveTime);

        alivePlayerCount++;

        StartCoroutine(InitData());
    }

    static void Init()
    {
        if(instance == null)
        {
            GameObject obj = GameObject.Find("GameManager");
            if(obj == null)
                obj = new GameObject { name = "GameManager" };

            instance = Util.GetORAddComponet<GameManager>(obj);
        }
    }

    public void StartWave()
    {
        isWave = true;
        StartCoroutine(WatiStopWave());
        StartWaveCall?.Invoke();
    }

    public void StopWave()
    {
        isWave = false;
        StopCoroutine(WatiStopWave());
        StopWaveCall?.Invoke();
    }

    public void GameOver()
    {
        // 게임 오버 씬으로 전환
    }

    WaitForSeconds waitWaveTime;
    IEnumerator WatiStopWave()
    {
        yield return waitWaveTime;
        StopWave();
    }

    IEnumerator InitData()
    {
        yield return new WaitForSeconds(1f);
        SetDataCall?.Invoke();
    } 
}

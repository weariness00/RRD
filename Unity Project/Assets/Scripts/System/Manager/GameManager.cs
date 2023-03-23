using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

    public PlayerController Player;

    public bool isWave = false;
    public float waveTime = 60f;

    public UnityEvent StartWaveCall;
    public UnityEvent StopWaveCall;

    private void Awake()
    {
        Instance = this;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
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

    IEnumerator WatiStopWave()
    {
        yield return new WaitForSeconds(waveTime);
        StopWave();
    }
}

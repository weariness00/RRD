using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector] public GameObject player;

    public bool isWaveStart = false;    // ���� ���̺갡 ���۵Ǿ�����
    public float endTime = 60f; // ���̺긦 ���߾� �ϴ� �ð�
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
        // ���� ���� ������ ��ȯ
    }

    WaitForSeconds endWaveTime;
    IEnumerator EndWaveTime()
    {
        yield return new WaitForSeconds(endTime);
        isWaveStart = false;
        EndWave();
    }
}

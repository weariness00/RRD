using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
	public static GameManager Instance { get { Init(); return instance; } }

    public Difficulty difficulty;
    [HideInInspector] public PlayerController Player;

    public AudioClip bgm;
    public Transform[] PlayerSpawnSpot; // 임시 나중에는 스테이지 별로 스크립트를 만들건데 거기서 다루기

    public bool isPause = false;
    public bool isWave = false;
    public float waveTime = 60f;

    [HideInInspector] public UnityEvent UpdateCall;
    [HideInInspector] public UnityEvent SetDataCall;
    [HideInInspector] public UnityEvent StartWaveCall;
    [HideInInspector] public UnityEvent StopWaveCall;

    [HideInInspector] public UnityEvent PauseCall;

    public int alivePlayerCount = 0;

    private void Awake()
    {
        difficulty = Util.GetORAddComponet<Difficulty>(gameObject);
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        waitWaveTime = new WaitForSeconds(waveTime);

        alivePlayerCount++;

        StartCoroutine(InitData());
        isPause = false;
    }

    private void Start()
    {
        Managers.Instance.StartCall += () => Managers.Sound.Play(bgm, SoundType.BGM);
     
        PlayerSpawn();
    }

    private void Update()
    {
        UpdateCall?.Invoke();

        if (Input.GetKeyDown(KeyCode.Escape))
            PauseCall?.Invoke();
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

    public void Pause()
    {
        isPause = !isPause;

        if(isPause)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
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

    public void GameEnd()
    {
        Pause();
        Managers.Scene.LoadScene(SceneType.GameEnd);
    }

    WaitForSeconds waitWaveTime;
    IEnumerator WatiStopWave()
    {
        yield return waitWaveTime;
        StopWave();
    }

    public void PlayerSpawn()
    {
        if (PlayerSpawnSpot == null) return;
        int index = Random.Range(0, PlayerSpawnSpot.Length);
        Player.transform.position = PlayerSpawnSpot[index].position;
    }

    IEnumerator InitData()
    {
        yield return new WaitForSeconds(1f);
        SetDataCall?.Invoke();
    } 
}

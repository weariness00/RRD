using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    float currentTime;   // 현재 웨이브 시작후 지난 시간

    private void Start()
    {
        text = Util.GetORAddComponet<TMP_Text>(gameObject);
        GameManager.Instance.StartWaveCall.AddListener(() => { currentTime = 0f; });
    }

    private void Update()
    {
        if (!GameManager.Instance.isWave)
            return;

        currentTime += Time.deltaTime;
        int minTime = (int)currentTime / 60;
        int secondTime = (int)currentTime % 60;
        text.text = $"{minTime} : {secondTime}";
    }
}

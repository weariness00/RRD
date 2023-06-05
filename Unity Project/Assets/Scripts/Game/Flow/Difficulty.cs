using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum DifficultyType
{
    None,
    Easy,
    Normal,
    Hard,
}

[System.Serializable]
public class DifficultyData
{
    public DifficultyType type;
    public float[] NextDifficultyTime;
}

public class Difficulty : MonoBehaviour
{
    public string path;

    public DifficultyData data;
    int index = 0;

    public ScrollRect timeView;
    float currentTime = 0;

    Vector2 endPosition;

    public void DifficultyUI_Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= data.NextDifficultyTime[index])
            currentTime = data.NextDifficultyTime[index];

        timeView.content.localPosition = 
            Vector2.Lerp(
                Vector2.zero,
                endPosition,
                currentTime / data.NextDifficultyTime[index]
            );
    }
    public void SetDifficulty(DifficultyType type)
    {
        if(type == 0)
            data = JsonUtil.LoadFile<DifficultyData>($"Difficulty/Easy");
        else
            data = JsonUtil.LoadFile<DifficultyData>($"Difficulty/{type.ToString()}");

        StartCoroutine("ChangeDifficulty");
        GameManager.Instance.UpdateCall.AddListener(DifficultyUI_Update);
    }

    IEnumerator ChangeDifficulty()
    {
        MonsterDataExcel monsterDataExcel = Managers.Excel.Load<MonsterDataExcel>("Monster/MonsterDataExcel");
        List<StatusData> difficultyMonsterData = null;
        switch (data.type)
        {
            case DifficultyType.None:
                difficultyMonsterData = monsterDataExcel.Easy_Status;
                break;
            case DifficultyType.Easy:
                difficultyMonsterData = monsterDataExcel.Easy_Status;
                break;
            case DifficultyType.Normal:
                difficultyMonsterData = monsterDataExcel.Normal_Status;
                break;
            case DifficultyType.Hard:
                difficultyMonsterData = monsterDataExcel.Hard_Status;
                break;
        }
        foreach (float time in data.NextDifficultyTime)
        {
            endPosition = new Vector2(-(index + 1) * timeView.content.GetComponent<GridLayoutGroup>().cellSize.x, timeView.content.localPosition.y);
            yield return new WaitForSeconds(time);
            ++index;
            MonsterSpawnManager.Instance.MonsterSpawnCall.AddListener((monster) =>
            {
                for (int i = 0; i < index; i++)
                    monster.status.AddData(difficultyMonsterData[monster.id]);
            });
        }
    }
}

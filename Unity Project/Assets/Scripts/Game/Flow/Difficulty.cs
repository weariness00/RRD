using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void SetDifficulty(DifficultyType type)
    {
        data = JsonUtil.LoadFile<DifficultyData>($"Difficulty/{type.ToString()}");
    }

    IEnumerator ChangeDifficulty()
    {
        yield return new WaitForSeconds(data.NextDifficultyTime[index++]);

    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowCharactorInfo : MonoBehaviour
{
    public static ShowCharactorInfo Instance;

    public Image charactorIcon;
    public TMP_Text charactorExplain;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowInfoUI(Sprite icon, string text)
    {
        charactorIcon.sprite = icon;
        charactorExplain.text = text;
    }
}

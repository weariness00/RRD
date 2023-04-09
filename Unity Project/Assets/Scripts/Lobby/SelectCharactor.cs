using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectCharactor : MonoBehaviour
{
    public GameObject charactorGrid;

    List<Toggle> charactorSlots = new List<Toggle>();

    int selectCharactorIndex = -1;

    private void Awake()
    {
        if (charactorGrid == null)
            charactorGrid = gameObject;
    }

    private void Start()
    {
        foreach (Toggle toggle in Util.GetChildren<Toggle>(charactorGrid))
        {
            int temp = ++selectCharactorIndex;
            toggle.onValueChanged.AddListener((bool isOn) => { if (isOn) CharactorIndex(temp); });

            charactorSlots.Add(toggle);
        }
    }
        
    public void CharactorIndex(int index)
    {
        selectCharactorIndex = index;
        ShowCharactorInfo.Instance.ShowInfoUI(charactorSlots[index].image.sprite, $"{index}");
    }
}

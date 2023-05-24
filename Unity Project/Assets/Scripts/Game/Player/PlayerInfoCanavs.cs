using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoCanavs : MonoBehaviour
{
	public Status status;
	public Slider hp_slider;
	public Slider mp_Slider;

    private void Start()
    {
        status = GameManager.Instance.Player.status;
    }

    public void OnChangeInfo()
	{
        hp_slider.value = Mathf.InverseLerp(0, status.maxHp.Cal(), status.hp.Cal());
        mp_Slider.value = Mathf.InverseLerp(0, status.maxMp.Cal(), status.mp.Cal());
    }
}

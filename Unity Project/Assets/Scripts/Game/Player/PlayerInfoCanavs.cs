using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoCanavs : MonoBehaviour
{
	public Status status;
    [Space]
	public Slider hp_slider;
    public TMP_Text hp_Value;
    [Space]
    public Slider mp_Slider;
    public TMP_Text mp_Value;
    [Space]
    public Slider exp_Slider;
    public TMP_Text exp_Value;
    public TMP_Text level_Value;

    private void Start()
    {
        status = GameManager.Instance.Player.status;
    }

    private void Update()
    {
        OnChangeInfo();
    }

    public void OnChangeInfo()
	{
        hp_slider.value = Mathf.InverseLerp(0, status.maxHp.Cal(), status.hp.Cal());
        mp_Slider.value = Mathf.InverseLerp(0, status.maxMp.Cal(), status.mp.Cal());
        exp_Slider.value = Mathf.InverseLerp(0, status.need_Exp, status.experience);

        hp_Value.text = status.hp.Cal().ToString();
        mp_Value.text = status.mp.Cal().ToString();
        exp_Value.text = status.experience.ToString();
        level_Value.text = status.level.ToString();
    }
}

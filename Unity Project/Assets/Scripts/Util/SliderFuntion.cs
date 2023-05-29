using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderFuntion : MonoBehaviour
{
	Slider slider;
    float Value;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        if (slider == null)
            Debug.LogWarning($"{name}��ü�� Slider Component�� �������� �ʽ��ϴ�.");
    }

    public void Value_Add(float value)
	{
        slider.value += value;
	}

    public void Value_Subtract(float value)
    {
        slider.value -= value;
    }

    public void Value_Memory()
    {
        Value = slider.value;
    }

    public void Value_ReturnMemory()
    {
        slider.value = Value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    private void Start()
    {
		Managers.Instance.UpdateCall += OnOff;
		gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        ++GameManager.Instance.OnWindowIndex;
        GameManager.Instance.GamePause();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnDisable()
    {
        --GameManager.Instance.OnWindowIndex;
        GameManager.Instance.GameResume();

        if(GameManager.Instance.OnWindowIndex.Equals(0))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void OnOff()
	{
		if (Managers.Key.InputActionDown(KeyToAction.Setting_UI))
		{
			gameObject.SetActive(!gameObject.activeSelf);
		}
	}
}

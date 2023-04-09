using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour
{

    private void Start()
    {
		gameObject.SetActive(false);
		GameManager.Instance.PauseCall.AddListener(OnUI);
    }

    public void OnUI()
	{
        gameObject.SetActive(!gameObject.activeSelf);
	}

	public void OnContinue()
	{
		Time.timeScale = 1;
		gameObject.SetActive(false);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
	PlayerController playerController;

	public GameObject waepon;

	protected void Init()
	{
        playerController = GetComponent<PlayerController>();
	}

	void Attack()
	{
		Debug.Log("╬Нец!");
    }
}

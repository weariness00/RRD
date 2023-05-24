using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
	PlayerController playerController;

	public GameObject waepon;
	[HideInInspector] public bool isAttackCancle = false;

	private void Start()
	{
        playerController = GetComponent<PlayerController>();
	}

	// 무기의 콜라이더를 활성화 해야함
	void Attack()
	{
        Debug.Log("어택!");
    }

	void Cancle()
	{
		isAttackCancle = true;
    }
}

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

	// ������ �ݶ��̴��� Ȱ��ȭ �ؾ���
	void Attack()
	{
        Debug.Log("����!");
    }

	void Cancle()
	{
		isAttackCancle = true;
    }
}

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
        playerController.equipment.weapon.GetComponent<BoxCollider>().enabled = true;

        Debug.Log("����!");
    }

	void Cancle()
	{
		isAttackCancle = true;
    }
}

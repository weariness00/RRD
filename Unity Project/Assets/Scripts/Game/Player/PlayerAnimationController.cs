using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
	PlayerController playerController;

	public GameObject waepon;

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
}

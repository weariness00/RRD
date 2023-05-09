using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
	Animator animaor;
	public GameObject waepon;

	protected void Init()
	{
		animaor = GetComponent<Animator>();
	}

	void Attack()
	{
		Debug.Log("╬Нец!");
	}
}

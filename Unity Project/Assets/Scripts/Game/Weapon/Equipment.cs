using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
	None,
	Weapon,
	// 의류
	// 악세서리
}

public class Equipment : MonoBehaviour
{
	public EquipmentType equipmenetType;
    public Equipment SetEquipment(Transform transform)
	{
		gameObject.transform.parent = transform;
		gameObject.SetActive(true);

		return this;
	}
}

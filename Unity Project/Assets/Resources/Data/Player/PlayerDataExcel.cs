using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class PlayerDataExcel : ScriptableObject
{
	public List<StatusData> Status; // Replace 'EntityType' to an actual type that is serializable.
	//public List<EntityType> Skill; // Replace 'EntityType' to an actual type that is serializable.
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class MonsterDataExcel : ScriptableObject
{
	public List<MonsterTypeData> Type; // Replace 'EntityType' to an actual type that is serializable.
	public List<StatusData> Status; // Replace 'EntityType' to an actual type that is serializable.
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class MonsterDataExcel : ScriptableObject
{
	public List<MonsterTypeData> Type; // Replace 'EntityType' to an actual type that is serializable.
	public List<StatusData> Default_Status; // Replace 'EntityType' to an actual type that is serializable.
	public List<StatusData> Easy_Status; // Replace 'EntityType' to an actual type that is serializable.
	public List<StatusData> Normal_Status; // Replace 'EntityType' to an actual type that is serializable.
	public List<StatusData> Hard_Status; // Replace 'EntityType' to an actual type that is serializable.
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class MonsterData : ScriptableObject
{
	public List<MonsterInfo> data; // Replace 'EntityType' to an actual type that is serializable.
}

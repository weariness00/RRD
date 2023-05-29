using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class StatusDataExcel : ScriptableObject
{
	public List<StatusData> Player; // Replace 'EntityType' to an actual type that is serializable.
}

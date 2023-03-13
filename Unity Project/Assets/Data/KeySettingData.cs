using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class KeySettingData : ScriptableObject
{
	public List<KeyDataInfo> Default; // Replace 'EntityType' to an actual type that is serializable.
}
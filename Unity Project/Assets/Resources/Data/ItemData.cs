using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class ItemData : ScriptableObject
{
	public List<Item> ItemSheet; // Replace 'EntityType' to an actual type that is serializable.
}
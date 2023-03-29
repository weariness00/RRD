using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MstItemEntity
{
	public int id;
	public string name;
	public string type;
	public string rate;
	public int price;
	public bool isNotForSale;
	public MstItemCategory category;  //속성?
}

public enum MstItemCategory
{
	Red,
	Green,
	Blue,
}
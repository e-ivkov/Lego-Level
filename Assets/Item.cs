using System;
using UnityEngine;
using System.Collections.Generic;

public class Item<T>
{
	public Vector2 Position 
	{
		private set;
		get;
	}

	public T Type 
	{
		private set;
		get;
	}

	public Item (Vector2 position, T type)
	{
		this.Position = position;
		this.Type = type;
	}
}



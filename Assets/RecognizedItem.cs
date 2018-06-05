using System;
using UnityEngine;
using System.Collections.Generic;

public class RecognizedItem: ICloneable
{
	public Vector2 Position 
	{
		private set;
		get;
	}

	public string Name 
	{
		private set;
		get;
	}

	public RecognizedItem (Vector2 position, string name)
	{
		this.Position = position;
		this.Name = name;
	}

	public object Clone()
	{
		return new RecognizedItem (Position, Name);
	}

	public override int GetHashCode ()
	{
		return Position.GetHashCode () ^ Name.GetHashCode();
	}

	public override bool Equals (object obj)
	{
		return ((RecognizedItem)obj).Position.Equals (Position) && ((RecognizedItem)obj).Name.Equals (Name);
	}
}



using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GridRecognizerFactory: RecognizerFactory<Color>
{
	public Vector2Int GridNumber { get; private set; }

	public List<Color> GridColors { 
		get;
		private set;
	}

	public GridRecognizerFactory (Vector2Int gridNumber, List<Color> gridColors)
	{
		GridNumber = gridNumber;
		GridColors = gridColors;
	}

	protected override IRecognizer<Color> MakeRecognizer ()
	{
		return new GridRecognizer (GridNumber, GridColors);
	}
}



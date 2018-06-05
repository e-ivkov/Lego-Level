using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GridRecognizer: IRecognizer<Color>
{

	public Vector2Int GridNumber { get; private set; }

	public List<Color> GridColors { 
		get;
		private set;
	}

	public GridRecognizer (Vector2Int gridNumber, List<Color> gridColors)
	{
		GridNumber = gridNumber;
		GridColors = gridColors.Select (color => new Color (color.r, color.g, color.b)).ToList ();
	}

	public List<RecognizedItem> Recognize(Texture2D image, Dictionary<Color, string> objectNames)
	{
		Vector4[,] averageColors = new Vector4[GridNumber.x, GridNumber.y];
		float nPixels = (image.width / GridNumber.x) * (image.height / GridNumber.y);
		for (int i = 0; i < image.width; i++) {
			for (int j = 0; j < image.height; j++) {
				averageColors[i/(image.width / GridNumber.x), j/(image.height / GridNumber.y)] += (Vector4)image.GetPixel(i,j);
			}
		}
		List<RecognizedItem> blocks = new List<RecognizedItem> ();
		for (int i = 0; i < averageColors.GetLength(0); i++) {
			for (int j = 0; j < averageColors.GetLength(1); j++) {
				averageColors[i, j] /= nPixels;
				blocks.Add(new RecognizedItem(new Vector2(i,j), objectNames[GridColors.OrderBy(color => Vector4.Distance(averageColors[i, j], color)).ToArray()[0]]));
			}
		}
		return blocks;
	}
}
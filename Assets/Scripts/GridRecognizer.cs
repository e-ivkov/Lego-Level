using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GridRecognizer : IRecognizer
{

    Vector2Int GridNumber;

    Dictionary<Color, string> ColorBlockNames;

    public GridRecognizer(Vector2Int gridNumber, Dictionary<Color, string> colorBlockNames)
    {
        GridNumber = gridNumber;
        ColorBlockNames = colorBlockNames;
    }

    public List<RecognizedItem> Recognize(Texture2D image)
    {
        var gridColors = ColorBlockNames.Keys;
        Vector4[,] averageColors = new Vector4[GridNumber.x, GridNumber.y];
        float nPixels = (image.width / GridNumber.x) * (image.height / GridNumber.y);
        for (int i = 0; i < image.width; i++)
        {
            for (int j = 0; j < image.height; j++)
            {
                //Debug.Log(i.ToString() + " " + j.ToString());
                averageColors[i / (int)Mathf.Ceil((float)image.width / (float)GridNumber.x), j / (int)Mathf.Ceil((float)image.height / (float)GridNumber.y)] += (Vector4)image.GetPixel(i, j);
            }
        }
        var blocks = new List<RecognizedItem>();
        for (int i = 0; i < averageColors.GetLength(0); i++)
        {
            for (int j = 0; j < averageColors.GetLength(1); j++)
            {
                averageColors[i, j] /= nPixels;
                blocks.Add(new RecognizedItem(new Vector2(i, j),
                    ColorBlockNames[gridColors.OrderBy(
                        color => Vector4.Distance(averageColors[i, j], color)).ToArray()[0]]));
            }
        }
        return blocks;
    }
}
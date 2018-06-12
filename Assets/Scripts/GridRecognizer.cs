using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GridRecognizer : IRecognizer
{

    protected Vector2Int GridNumber;

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

        for (int i = 0; i < image.width; i++)
        {
            for (int j = 0; j < image.height; j++)
            {
                //Debug.Log(i.ToString() + " " + j.ToString());
                Vector2Int blockIndexes = GetBlockIndexes(new Vector2Int(i, j), image);
                if(blockIndexes.x >= 0 && blockIndexes.y >= 0)
                    averageColors[blockIndexes.x, blockIndexes.y] += (Vector4)image.GetPixel(i, j);
            }
        }
        var blocks = new List<RecognizedItem>();
        for (int i = 0; i < averageColors.GetLength(0); i++)
        {
            for (int j = 0; j < averageColors.GetLength(1); j++)
            {
                averageColors[i, j] /= GetNPixelsPerBlock(image);
                blocks.Add(new RecognizedItem(new Vector2(i, j),
                    ColorBlockNames[gridColors.OrderBy(
                        color => Vector4.Distance(averageColors[i, j], color)).ToArray()[0]]));
            }
        }
        return blocks;
    }

    public virtual float GetNPixelsPerBlock(Texture2D image){
        float nPixels = (image.width / GridNumber.x) * (image.height / GridNumber.y);;
        return nPixels;
    }

    public virtual Vector2Int GetBlockIndexes(Vector2Int pixelIndexes, Texture2D image){
        int x = pixelIndexes.x / (int)Mathf.Ceil((float)image.width / (float)GridNumber.x);
        int y = pixelIndexes.y / (int)Mathf.Ceil((float)image.height / (float)GridNumber.y);
        return new Vector2Int(x, y);
    }
}
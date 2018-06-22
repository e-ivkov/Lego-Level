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

    public List<RecognizedItem> Recognize(Color[,] image)
    {
        var gridColors = ColorBlockNames.Keys;
        Vector4[,] averageColors = new Vector4[GridNumber.x, GridNumber.y];
        int width = image.GetLength(0); // NOTE: not sure if the order of dimensions is right
        int height = image.GetLength(1);
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                //Debug.Log(i.ToString() + " " + j.ToString());
                Vector2Int blockIndexes = GetBlockIndexes(new Vector2Int(i, j), image);
                if(blockIndexes.x >= 0 && blockIndexes.y >= 0)
                    averageColors[blockIndexes.x, blockIndexes.y] += (Vector4)image[i, j];
            }
        }
        var blocks = new List<RecognizedItem>();
        for (int i = 0; i < averageColors.GetLength(0); i++)
        {
            for (int j = 0; j < averageColors.GetLength(1); j++)
            {
                averageColors[i, j] /= GetNPixelsPerBlock(image);
                averageColors[i, j].w = 0;
                blocks.Add(new RecognizedItem(new Vector2(i, j),
                    ColorBlockNames[gridColors.OrderBy(
                        color => Vector4.Distance(averageColors[i, j], color)).ToArray()[0]]));
            }
        }
        return blocks;
    }

    /// <summary>
    /// Gets the number of pixels per block.
    /// </summary>
    /// <returns>number of pixels for the given block</returns>
    /// <param name="image">Image.</param>
    public virtual float GetNPixelsPerBlock(Color[,] image){
        float nPixels = ((float)image.GetLength(0) / (float)GridNumber.x) * ((float)image.GetLength(1) / (float)GridNumber.y);
        return nPixels;
    }

    /// <summary>
    /// Gets the indexes of the block that contains this pixel
    /// </summary>
    /// <returns>The block indexes</returns>
    /// <param name="pixelIndexes">Position of the pixel</param>
    /// <param name="image">Image.</param>
    public virtual Vector2Int GetBlockIndexes(Vector2Int pixelIndexes, Color[,] image){
        int x = pixelIndexes.x / (int)Mathf.Ceil((float)image.GetLength(0) / (float)GridNumber.x);
        int y = pixelIndexes.y / (int)Mathf.Ceil((float)image.GetLength(1) / (float)GridNumber.y);
        return new Vector2Int(x, y);
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

public class SkewedGridRecognizer : GridRecognizer
{
    public Vector2[] Corners { get; set; } //in clockwise order, starting from topLeft

    private Vector2[,] intersectionPoints;
    
    public SkewedGridRecognizer(Vector2Int gridNumber, Dictionary<Color, string> colorBlockNames, Vector2[] corners) : base(gridNumber, colorBlockNames)
    {
        intersectionPoints = new Vector2[GridNumber.x, GridNumber.y];
        Corners = corners;
        for (int i = 0; i < GridNumber.x; i++)
        {
            for (int j = 0; j < GridNumber.y; j++)
            {
                var A1 = (Corners[1] - Corners[0]) / GridNumber.x * i + Corners[0];
                var A2 = (Corners[2] - Corners[3]) / GridNumber.x * i + Corners[3];
                var B1 = (Corners[0] - Corners[3]) / GridNumber.y * j + Corners[3];
                var B2 = (Corners[1] - Corners[2]) / GridNumber.y * j + Corners[2];
                bool exists = false;
                Debug.Log(i.ToString() + " " + j.ToString());
                intersectionPoints[i, j] = Geometry2D.GetIntersectionPointCoordinates(A1, A2, B1, B2, out exists);
            }
        }
    }

    public override Vector2Int GetBlockIndexes(Vector2Int pixelIndexes, Color[,] image)
    {
        
        for (int i = 0; i < GridNumber.x - 1; i++)
        {
            for (int j = 0; j < GridNumber.y - 1; j++)
            {
                var polygon = new Vector2[] { intersectionPoints[i, j], intersectionPoints[i, j + 1], intersectionPoints[i + 1, j + 1], intersectionPoints[i + 1, j] };
                if (Geometry2D.IsPointInPolygon(pixelIndexes, polygon)) return new Vector2Int(i, j);
            }
        }
        return new Vector2Int(-1,-1); //pixel is not in polygon
    }
}

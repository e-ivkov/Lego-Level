using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SGRFactory : RecognizerFactory
{
    readonly Vector2Int GridNumber;
    readonly Dictionary<Color, string> ColorBlockNames;
    readonly Vector2[] Corners;

    public SGRFactory(Vector2Int gridNumber, Dictionary<Color, string> colorBlockNames, Vector2[] corners)
    {
        GridNumber = gridNumber;
        ColorBlockNames = colorBlockNames;
        Corners = corners;
    }

    protected override IRecognizer MakeRecognizer()
    {
        var rec = new SkewedGridRecognizer(GridNumber, ColorBlockNames)
        {
            Corners = Corners
        };
        return rec;
    }
}



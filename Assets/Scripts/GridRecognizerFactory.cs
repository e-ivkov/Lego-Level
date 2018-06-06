using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GridRecognizerFactory : RecognizerFactory
{
    Vector2Int GridNumber;

    Dictionary<Color, string> ColorBlockNames;

    public GridRecognizerFactory(Vector2Int gridNumber, Dictionary<Color, string> colorBlockNames)
    {
        GridNumber = gridNumber;
        ColorBlockNames = colorBlockNames;
    }

    protected override IRecognizer MakeRecognizer()
    {
        return new GridRecognizer(GridNumber, ColorBlockNames);
    }
}



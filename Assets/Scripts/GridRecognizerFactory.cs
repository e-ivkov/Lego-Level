using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GridRecognizerFactory : RecognizerFactory
{
    Vector2Int GridNumber;

    Dictionary<Color, string> ColorBlockNames;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:GridRecognizerFactory"/> class.
    /// </summary>
    /// <param name="gridNumber">Dimensions of the grid for the recognizer</param>
    /// <param name="colorBlockNames">Color block names, the names will be returned in the list of reoognized items if the blocks of the corresponding color are found</param>
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



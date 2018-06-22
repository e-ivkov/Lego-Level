using System;
using System.Collections.Generic;
using UnityEngine;

public interface IRecognizer
{
    /// <summary>
    /// Recognize the items on the given image
    /// </summary>
    /// <returns>The list of recognized items</returns>
    /// <param name="image">Image.</param>
    List<RecognizedItem> Recognize(Color[,] image);
}



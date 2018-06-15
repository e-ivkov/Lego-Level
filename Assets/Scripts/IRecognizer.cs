using System;
using System.Collections.Generic;
using UnityEngine;

public interface IRecognizer
{
    List<RecognizedItem> Recognize(Color[,] image);
}



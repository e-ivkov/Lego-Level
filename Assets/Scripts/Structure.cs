using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public struct Structure
{
    public int Priority;

    public List<RecognizedItem> Blocks;

    public string Name;

    public Structure(int priority, List<RecognizedItem> blocks, string name)
    {
        Priority = priority;
        Blocks = blocks;
        Name = name;
    }

    public Structure(int priority, Texture2D blocks, Dictionary<Color, string> colorNames, string name)
    {
        Priority = priority;
        Blocks = new List<RecognizedItem>();
        for (int i = 0; i < blocks.width; i++)
        {
            for (int j = 0; j < blocks.height; j++)
            {
                if (blocks.GetPixel(i, j).a > 0)
                    Blocks.Add(new RecognizedItem(new Vector2(i, j), colorNames[blocks.GetPixel(i, j)]));
            }
        }
        Name = name;
    }
}



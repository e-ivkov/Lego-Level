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
                Debug.Log(blocks.GetPixel(i, j));
                if (blocks.GetPixel(i, j) != Color.white)
                {
                    var c = blocks.GetPixel(i, j);
                    c.a = 0;
                    if(colorNames.ContainsKey(c))
                        Blocks.Add(new RecognizedItem(new Vector2(i, j), colorNames[c]));
                }
            }
        }
        Name = name;
    }
}



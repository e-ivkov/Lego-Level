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
}



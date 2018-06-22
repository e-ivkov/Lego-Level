using System;
using UnityEngine;

[Serializable]
public struct ImportStructure
{
    /// <summary>
    /// The image of the structure, each pixel represents a block
    /// </summary>
    public Texture2D texture;
    public String name;

    /// <summary>
    /// The priority influences which structures recognition should be prefered over which if the conflicts arise.
    /// </summary>
    public int priority;
}


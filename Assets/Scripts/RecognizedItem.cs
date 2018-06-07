using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public struct RecognizedItem
{
    public Vector2 Position;

    public string Name;


    public RecognizedItem(Vector2 position, string name)
    {
        this.Position = position;
        this.Name = name;
    }

    public object Clone()
    {
        return new RecognizedItem(Position, Name);
    }

    public override int GetHashCode()
    {
        return Position.GetHashCode() ^ Name.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        if (obj is RecognizedItem)
            return ((RecognizedItem)obj).Position.Equals(Position) && ((RecognizedItem)obj).Name.Equals(Name);
        return false;
    }
}



using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class StructureRecognizer : IRecognizer
{
    RecognizerFactory Factory;

    List<Structure> Structures;

    public StructureRecognizer(List<Structure> structures, RecognizerFactory blockFactory)
    {
        Structures = structures;
        Factory = blockFactory;
    }

    public List<RecognizedItem> Recognize(Texture2D image)
    {
        var blocks = new HashSet<RecognizedItem>(Factory.GetObject().Recognize(image));
        var coords = blocks.ToList().Select(block => new Vector2(block.Position.x, block.Position.y));
        var recognizedStructures = new List<RecognizedItem>();
        foreach (Vector2 point in coords)
        {
            foreach (Structure structure in Structures.OrderBy(structure => structure.Priority))
            {
                var structureBlocks = structure.Blocks.Select(block => new RecognizedItem(block.Position + point, block.Name));
                if (structureBlocks.All(structureBlock => blocks.Contains(structureBlock)))
                {
                    recognizedStructures.Add(new RecognizedItem(point, structure.Name));
                    var sBlocksSet = new HashSet<RecognizedItem>(structureBlocks);
                    blocks.RemoveWhere(block => sBlocksSet.Contains(block));
                }
            }
        }
        return recognizedStructures;
    }

}



using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class StructureRecognizer : IRecognizer
{
    RecognizerFactory Factory;

    List<Structure> Structures;

    private float precision;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StructureRecognizer"/> class.
    /// </summary>
    /// <param name="structures">Structures.</param>
    /// <param name="blockFactory">Block factory.</param>
    /// <param name="precision">Precision of structure recognition, influences how much blocks should be present there for it to be still recognized</param>
    public StructureRecognizer(List<Structure> structures, RecognizerFactory blockFactory, float precision)
    {
        Structures = structures;
        Factory = blockFactory;
        this.precision = precision;
    }

    public List<RecognizedItem> Recognize(Color[,] image)
    {
        var blocks = new HashSet<RecognizedItem>(Factory.GetObject().Recognize(image));
        var coords = blocks.ToList().Select(block => new Vector2(block.Position.x, block.Position.y));
        var recognizedStructures = new List<RecognizedItem>();
        foreach (Vector2 point in coords)
        {
            foreach (Structure structure in Structures.OrderBy(structure => structure.Priority))
            {
                var structureBlocks = structure.Blocks.Select(block => new RecognizedItem(block.Position + point, block.Name));
                if (structureBlocks.Count(structureBlock => blocks.Contains(structureBlock)) >= precision * structureBlocks.Count())
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



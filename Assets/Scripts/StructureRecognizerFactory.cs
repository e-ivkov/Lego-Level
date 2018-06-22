using System;
using System.Collections.Generic;

public class StructureRecognizerFactory : RecognizerFactory
{
    RecognizerFactory Factory;

    List<Structure> Structures;

    private float precision;
    /// <summary>
    /// Initializes a new instance of the <see cref="T:StructureRecognizerFactory"/> class.
    /// </summary>
    /// <param name="structures">Structures.</param>
    /// <param name="blockFactory">Block factory.</param>
    /// <param name="precision">Precision of structure recognition, influences how much blocks should be present there for it to be still recognized</param>
    public StructureRecognizerFactory(List<Structure> structures, RecognizerFactory blockFactory, float precision)
    {
        Structures = structures;
        Factory = blockFactory;
        this.precision = precision;
    }

    protected override IRecognizer MakeRecognizer()
    {
        return new StructureRecognizer(Structures, Factory, precision);
    }
}


using System;
using System.Collections.Generic;

public class StructureRecognizerFactory : RecognizerFactory
{
    RecognizerFactory Factory;

    List<Structure> Structures;

    private float precision;

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


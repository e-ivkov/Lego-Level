using System;
using System.Collections.Generic;

public class StructureRecognizerFactory: RecognizerFactory
{
	RecognizerFactory Factory;

	List<Structure> Structures;

	public StructureRecognizerFactory (List<Structure> structures, RecognizerFactory blockFactory)
	{
		Structures = structures;
		Factory = blockFactory;
	}

	protected override IRecognizer MakeRecognizer ()
	{
		return new StructureRecognizer (Structures, Factory);
	}
}


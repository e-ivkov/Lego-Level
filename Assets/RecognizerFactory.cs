using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RecognizerFactory
{
	protected abstract IRecognizer MakeRecognizer ();

	public IRecognizer GetObject()
	{
		return this.MakeRecognizer ();
	}
}

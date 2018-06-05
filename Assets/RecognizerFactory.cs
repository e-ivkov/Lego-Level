using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RecognizerFactory<T> 
{
	protected abstract IRecognizer<T> MakeRecognizer ();

	public IRecognizer<T> GetObject()
	{
		return this.MakeRecognizer ();
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;

public interface IRecognizer<T>
{
	List<Item<T>> Recognize(Texture2D image);
}



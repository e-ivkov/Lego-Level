using System;
using System.Collections.Generic;
using System.Linq;

static class Extensions
{
	public static IEnumerable<T> Clone<T>(this IEnumerable<T> listToClone) where T: ICloneable
	{
		return listToClone.Select(item => (T)item.Clone()).ToList();
	}
}


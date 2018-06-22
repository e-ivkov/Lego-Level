using System;
using System.Collections.Generic;
using System.Linq;

static class Extensions
{
    /// <summary>
    /// Clone the specified listToClone.
    /// </summary>
    /// <returns>The clone of the given list</returns>
    /// <param name="listToClone">List to clone.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static IEnumerable<T> Clone<T>(this IEnumerable<T> listToClone) where T : ICloneable
    {
        return listToClone.Select(item => (T)item.Clone()).ToList();
    }
}


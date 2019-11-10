using System;
using System.Collections.Generic;

namespace ClassLib
{
    public static class EnumerableCloner
    {
        public static IEnumerable<T> DeepClone<T>(this IEnumerable<T> iEnumerable) where T : ICloneable
        {
            foreach (var element in iEnumerable) yield return (T)element.Clone();
        }
    }
}

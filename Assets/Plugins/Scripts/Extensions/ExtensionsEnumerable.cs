using System.Collections.Generic;
using System;
using System.Linq;

public static class ExtensionsEnumerable
{
    public static IEnumerable<T> ChangeEach<T>(this IEnumerable<T> array, Func<T, T> mutator) => array.ToArray().ChangeEach(mutator);
}

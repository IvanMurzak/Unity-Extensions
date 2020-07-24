using System;
using System.Collections.Generic;

public static class ExtansionsHashSet
{
    public static T GetRandom<T>(this HashSet<T> hashSet)
    {
        var index = UnityEngine.Random.Range(0, hashSet.Count);
        var iter = hashSet.GetEnumerator();
        iter.MoveNext();
        for (int i = 0; i < index; i++)
            iter.MoveNext();
        return iter.Current;
    }

    public static HashSet<T> Clone<T>(this HashSet<T> hashSet)
    {
        var newHashSet = new HashSet<T>();
        newHashSet.UnionWith(hashSet);
        return newHashSet;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

public static class ExtensionsLinkedList
{
    public static T GetRandom<T>(this LinkedList<T> list)
    {
        return list.ElementAt(UnityEngine.Random.Range(0, list.Count));
    }

    public static LinkedList<T> ChangeEach<T>(this LinkedList<T> list, Func<T, T> mutator)
    {
        for(var iter = list.First; iter != null; iter = iter.Next)
        {
            iter.Value = mutator(iter.Value);
        }
        return list;
    }

    public static LinkedList<T> Clone<T>(this LinkedList<T> list)
    {
        var newList = new LinkedList<T>();
        foreach (var item in list)
            newList.AddLast(item);
        return newList;
    }
}

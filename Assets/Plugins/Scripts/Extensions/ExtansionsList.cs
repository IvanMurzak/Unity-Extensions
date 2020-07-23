using System;
using System.Collections.Generic;
using System.Linq;

public static class ExtansionsList
{
    public static T			Random<T>		(this List<T> list)
    {
        return list[UnityEngine.Random.Range(0, list.Count)];
    }
    public static List<T>	ChangeEach<T>	(this List<T> list, Func<T, T> mutator)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i] = mutator(list[i]);
        }
        return list;
    }
	public static List<T>	Clone<T>		(this List<T> listToClone) where T : ICloneable
	{
		return listToClone.Select(item => (T)item.Clone()).ToList();
	}
	public static IList<T>	Clone<T>		(this IList<T> listToClone) where T : ICloneable
	{
		return listToClone.Select(item => (T)item.Clone()).ToList();
	}
	public static T			First<T>		(this List<T> list) => list.Count > 0 ? list[0]					: default(T);
	public static T			Last<T>			(this List<T> list)	=> list.Count > 0 ? list[list.Count - 1]	: default(T);
}

using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;

public static class ObservableExtensions
{
	public static IObservable<T> WhereNotNull<T>(this IObservable<T> source)
	{
		return source.Where(x => x != null);
	}
	public static IEnumerable<TSource> WhereNotNull<TSource>(this IEnumerable<TSource> source)
	{
		return source.Where(x => x != null);
	}
}

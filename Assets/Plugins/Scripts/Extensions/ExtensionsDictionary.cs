using System.Collections.Generic;
using System;

public static class ExtensionsDictionary
{
	public static TValue GetOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> original, TKey key, TValue defaultValue = default)
	{
		if (original.ContainsKey(key))	return original[key];
		else							return defaultValue;
	}

	public static bool ContainsAndEquals<TKey, TValue>(this Dictionary<TKey, TValue> original, TKey key, TValue value)
	{
		if (original.ContainsKey(key))
		{
			var originalValue = original[key];
			if (originalValue == null)
			{
				return value == null;
			}
			else
			{
				return originalValue.Equals(value);
			}
		}
		return false;
	}

	public static Dictionary<TKey, TValue> Copy<TKey, TValue>(this Dictionary<TKey, TValue> original)
    {
        var ret = new Dictionary<TKey, TValue>(original.Count, original.Comparer);
        foreach (var entry in original)
            ret.Add(entry.Key, entry.Value);
        return ret;
    }

    public static Dictionary<TKey, TValue> Clone<TKey, TValue>(this Dictionary<TKey, TValue> original) where TValue : ICloneable
    {
        var ret = new Dictionary<TKey, TValue>(original.Count, original.Comparer);
        foreach (var entry in original)
            ret.Add(entry.Key, (TValue)entry.Value.Clone());
        return ret;
    }

	public static SerializableDictionary<TKey, TValue> Copy<TKey, TValue>(this SerializableDictionary<TKey, TValue> original)
	{
		var ret = new SerializableDictionary<TKey, TValue>(original.Count, original.Comparer);
		foreach (var entry in original)
			ret.Add(entry.Key, entry.Value);
		return ret;
	}

	public static SerializableDictionary<TKey, TValue> Clone<TKey, TValue>(this SerializableDictionary<TKey, TValue> original) where TValue : ICloneable
	{
		var ret = new SerializableDictionary<TKey, TValue>(original.Count, original.Comparer);
		foreach (var entry in original)
			ret.Add(entry.Key, (TValue)entry.Value.Clone());
		return ret;
	}
}
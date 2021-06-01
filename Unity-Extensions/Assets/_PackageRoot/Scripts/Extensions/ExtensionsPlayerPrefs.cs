using UnityEngine;
using System;
using BigInt = System.Numerics.BigInteger;

public class PlayerPrefsEx
{
    public static bool GetBool(string key, bool defaultValue = false)
    {
        return PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
    }
    public static void SetBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }

	public static DateTime GetDateTime(string key, DateTime defaultValue)
	{
		var str = PlayerPrefs.GetString(key, null);
		if (str == null) return defaultValue;

		DateTime result;
		if (DateTime.TryParse(str, out result))
			return result;
		return defaultValue;
	}
	public static void SetDateTime(string key, DateTime value)
	{
		PlayerPrefs.SetString(key, value.ToString());
	}

	public static BigInt GetBigInt(string key, BigInt defaultValue)
	{
		var str = PlayerPrefs.GetString(key, null);
		if (str == null) return defaultValue;

		BigInt result;
		if (BigInt.TryParse(str, out result))
			return result;
		return defaultValue;
	}
	public static void SetBigInt(string key, BigInt value)
	{
		PlayerPrefs.SetString(key, value.ToString());
	}

	public static Vector3 GetVector3(string key, Vector3 defaultValue)
	{
		var str = PlayerPrefs.GetString(key, null);
		if (str == null) return defaultValue;

		var strs = str.Split(':');
		if (strs.Length != 3) return defaultValue;

		float x, y, z;
		if (float.TryParse(strs[0], out x))
		{
			if (float.TryParse(strs[1], out y))
			{
				if (float.TryParse(strs[2], out z))
				{
					return new Vector3(x, y, z);
				}
			}
		}
		return defaultValue;
	}
	public static void SetVector3(string key, Vector3 value)
	{
		PlayerPrefs.SetString(key, $"{value.x}:{value.y}:{value.z}");
	}

	public static Vector3 GetVector2(string key, Vector2 defaultValue)
	{
		var str = PlayerPrefs.GetString(key, null);
		if (str == null) return defaultValue;

		var strs = str.Split(':');
		if (strs.Length != 2) return defaultValue;

		float x, y;
		if (float.TryParse(strs[0], out x))
		{
			if (float.TryParse(strs[1], out y))
			{
				return new Vector2(x, y);
			}
		}
		return defaultValue;
	}
	public static void SetVector2(string key, Vector2 value)
	{
		PlayerPrefs.SetString(key, $"{value.x}:{value.y}");
	}
}
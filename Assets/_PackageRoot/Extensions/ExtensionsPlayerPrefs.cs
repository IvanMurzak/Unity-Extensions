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
}

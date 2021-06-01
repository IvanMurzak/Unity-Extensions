using System;
using UnityEngine;
using BigInt = System.Numerics.BigInteger;

//public class PlayerPrefsExUnit<T>
//{
//	public	string		Key				{ get; private set; }
//	public	T			DefaultValue	{ get; private set; }
//	public	T			Value
//	{
//		get => PlayerPrefs.GetString(Key, DefaultValue.ToString())
//		set
//	}

//	public PlayerPrefsExUnit(string key, T defaultValue)
//	{
//		this.Key			= key;
//		this.DefaultValue	= defaultValue;
//	}
//}

public class UniqueHash
{
#if UNITY_EDITOR
	public static int Hash => 1111;
#else
	public static int Hash => SystemInfo.deviceUniqueIdentifier.GetHashCode();
#endif
}

public class PlayerPrefsExInt
{
	public	string		Key				{ get; private set; }
	public	int			DefaultValue	{ get; private set; }
	public	int			Value
	{
		get => PlayerPrefs.GetInt(Key, DefaultValue);
		set => PlayerPrefs.SetInt(Key, value);
	}

	public PlayerPrefsExInt(string key, int defaultValue = 0)
	{
		this.Key			= key + UniqueHash.Hash;
		this.DefaultValue	= defaultValue;
	}
}
public class PlayerPrefsExFloat
{
	public	string		Key				{ get; private set; }
	public	float		DefaultValue	{ get; private set; }
	public	float		Value
	{
		get => PlayerPrefs.GetFloat(Key, DefaultValue);
		set => PlayerPrefs.SetFloat(Key, value);
	}

	public PlayerPrefsExFloat(string key, float defaultValue = 0f)
	{
		this.Key			= key + UniqueHash.Hash;
		this.DefaultValue	= defaultValue;
	}
}
public class PlayerPrefsExString
{
	public	string		Key				{ get; private set; }
	public	string		DefaultValue	{ get; private set; }
	public	string		Value
	{
		get => PlayerPrefs.GetString(Key, DefaultValue);
		set => PlayerPrefs.SetString(Key, value);
	}

	public PlayerPrefsExString(string key, string defaultValue = null)
	{
		this.Key			= key + UniqueHash.Hash;
		this.DefaultValue	= defaultValue;
	}
}
public class PlayerPrefsExBool
{
	public	string		Key				{ get; private set; }
	public	bool		DefaultValue	{ get; private set; }
	public	bool		Value
	{
		get => PlayerPrefsEx.GetBool(Key, DefaultValue);
		set => PlayerPrefsEx.SetBool(Key, value);
	}

	public PlayerPrefsExBool(string key, bool defaultValue = false)
	{
		this.Key			= key + UniqueHash.Hash;
		this.DefaultValue	= defaultValue;
	}
}
public class PlayerPrefsExDateTime
{
	public	string		Key				{ get; private set; }
	public	DateTime	DefaultValue	{ get; private set; }
	public	DateTime	Value
	{
		get => PlayerPrefsEx.GetDateTime(Key, DefaultValue);
		set => PlayerPrefsEx.SetDateTime(Key, value);
	}

	public PlayerPrefsExDateTime(string key, DateTime defaultValue)
	{
		this.Key			= key + UniqueHash.Hash;
		this.DefaultValue	= defaultValue;
	}
}
public class PlayerPrefsExBigInt
{
	public	string		Key				{ get; private set; }
	public	BigInt		DefaultValue	{ get; private set; }
	public	BigInt		Value
	{
		get => PlayerPrefsEx.GetBigInt(Key, DefaultValue);
		set => PlayerPrefsEx.SetBigInt(Key, value);
	}

	public PlayerPrefsExBigInt(string key, BigInt defaultValue)
	{
		this.Key			= key + UniqueHash.Hash;
		this.DefaultValue	= defaultValue;
	}
}
public class PlayerPrefsExVector3
{
	public	string		Key				{ get; private set; }
	public	Vector3		DefaultValue	{ get; private set; }
	public	Vector3		Value
	{
		get => PlayerPrefsEx.GetVector3(Key, DefaultValue);
		set => PlayerPrefsEx.SetVector3(Key, value);
	}

	public PlayerPrefsExVector3(string key, Vector3 defaultValue)
	{
		this.Key			= key + UniqueHash.Hash;
		this.DefaultValue	= defaultValue;
	}
}
public class PlayerPrefsExVector2
{
	public	string		Key				{ get; private set; }
	public	Vector2		DefaultValue	{ get; private set; }
	public	Vector2		Value
	{
		get => PlayerPrefsEx.GetVector2(Key, DefaultValue);
		set => PlayerPrefsEx.SetVector2(Key, value);
	}

	public PlayerPrefsExVector2(string key, Vector2 defaultValue)
	{
		this.Key			= key + UniqueHash.Hash;
		this.DefaultValue	= defaultValue;
	}
}
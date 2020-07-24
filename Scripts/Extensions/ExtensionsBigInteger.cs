using UnityEngine;
using BigInt = System.Numerics.BigInteger;

public static class ExtensionsBigInteger
{
    public static bool IsNegative		(this BigInt v) => v.Sign < 0;
    public static bool IsPositive		(this BigInt v) => v.Sign > 0;
    public static bool IsZeroOrNegative	(this BigInt v) => v.Sign <= 0;
    
    public static float DivideInRangeFromZeroToOne(this BigInt v, BigInt o, int digits = 2)
    {
        if (v.IsZeroOrNegative()) return 0;
        var powDigits = (int)Mathf.Pow(10, digits);
        float result = (float)((v * powDigits) / o);
        return result / powDigits;
    }

	public static BigInt AddPercents(this BigInt v, int percents)
	{
		var percent = v / 100;
		v += percent * percents;
		return v;
	}

	public static float Clamp(this BigInt v, ref BigInt min, ref BigInt max, int digits = 2) => (v - min).DivideInRangeFromZeroToOne(max, digits);
}

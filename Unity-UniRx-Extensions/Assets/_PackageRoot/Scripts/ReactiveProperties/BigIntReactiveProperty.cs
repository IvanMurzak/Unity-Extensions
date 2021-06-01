using System;
using UniRx;
using BigInt = System.Numerics.BigInteger;

public class BigIntReactiveProperty : ReactiveProperty<BigInt>
{
    public BigIntReactiveProperty	(byte[]		value) : base(new BigInt(value))	{ }
    public BigIntReactiveProperty	(decimal	value) : base(new BigInt(value))	{ }
    public BigIntReactiveProperty	(double		value) : base(new BigInt(value))	{ }
    public BigIntReactiveProperty	(int		value) : base(new BigInt(value))	{ }
    public BigIntReactiveProperty	(long		value) : base(new BigInt(value))	{ }
    public BigIntReactiveProperty	(float		value) : base(new BigInt(value))	{ }
    public BigIntReactiveProperty	(uint		value) : base(new BigInt(value))	{ }
    public BigIntReactiveProperty	(ulong		value) : base(new BigInt(value))	{ }
    public BigIntReactiveProperty	(string		value) : base(BigInt.Parse(value))	{ }
    public BigIntReactiveProperty	(BigInt		value) : base(value)				{ }
}
using System;
using UnityEngine;


public static class ExtensionsBoundsInt
{
    public static Bounds ToBounds(this BoundsInt v) => new Bounds(v.center, v.size);
}

using System;
using UnityEngine;


/// <summary>
/// Extension methods for UnityEngine.Vector2Int.
/// </summary>
public static class ExtensionsVector3Int
{
    /// <summary>
    /// Sets X value to the Vector2Int.
    /// </summary>
    /// <param name="v">Destination Vector2Int.</param>
    /// <param name="x">X value.</param>
    public static Vector3Int SetX(this Vector3Int v, int x)
    {
        v.x = x;
        return v;
    }

    /// <summary>
    /// Sets Y value to the Vector2Int.
    /// </summary>
    /// <param name="v">Destination Vector2Int.</param>
    /// <param name="y">Y value.</param>
    public static Vector3Int SetY(this Vector3Int v, int y)
    {
        v.y = y;
        return v;
    }

    /// <summary>
    /// Sets Z value to the Vector3Int.
    /// </summary>
    /// <param name="v">Destination Vector3.</param>
    /// <param name="z">Z value.</param>
    public static Vector3Int SetZ(this Vector3Int v, int z)
    {
        v.z = z;
        return v;
    }

    public static Vector3Int AddToInt(this Vector3Int v, Vector2 o)
    {
        return new Vector3Int((int)(v.x + o.x), (int)(v.y + o.y), v.x);
    }

    public static Vector3 Add(this Vector3Int v, Vector2 o)
    {
        return new Vector3(v.x + o.x, v.y + o.y, v.z);
    }

    public static Vector3Int AddToInt(this Vector3Int v, Vector2Int o)
    {
        return new Vector3Int((int)(v.x + o.x), (int)(v.y + o.y), (int)(v.z));
    }

    public static Vector3 Add(this Vector3Int v, Vector3 o)
    {
        return new Vector3(v.x + o.x, v.y + o.y, v.z + o.z);
    }




    public static Vector3Int MultiplyToInt(this Vector3Int v, Vector2 o)
    {
        return new Vector3Int((int)(v.x * o.x), (int)(v.y * o.y), v.x);
    }

    public static Vector3 Multiply(this Vector3Int v, Vector2 o)
    {
        return new Vector3(v.x * o.x, v.y * o.y, v.z);
    }

    public static Vector3Int MultiplyToInt(this Vector3Int v, Vector2Int o)
    {
        return new Vector3Int((int)(v.x * o.x), (int)(v.y * o.y), (int)(v.z));
    }

    public static Vector3 Multiply(this Vector3Int v, Vector3 o)
    {
        return new Vector3(v.x * o.x, v.y * o.y, v.z * o.z);
    }



    public static Vector3Int ModuleDivide(this Vector3Int v, int o)
    {
        return new Vector3Int(v.x % o, v.y % o, v.z % o);
    }
    public static Vector3Int ModuleDivide(this Vector3Int v, Vector3Int o)
    {
        return new Vector3Int(v.x % o.x, v.y % o.y, v.z % o.z);
    }



    public static Vector3Int Abs(this Vector3Int v)
    {
        return new Vector3Int(Math.Abs(v.x), Math.Abs(v.y), Math.Abs(v.z));
    }
}

using Extensions.Utils;
using UnityEngine;

/// <summary>
/// Extension methods for UnityEngine.Vector2.
/// </summary>
public static class ExtensionsVector2
{
    /// <summary>
    /// Sets X value to the Vector2.
    /// </summary>
    /// <param name="v">Destination Vector2.</param>
    /// <param name="x">X value.</param>
    public static Vector2 SetX(this Vector2 v, float x)
    {
        v.x = x;
        return v;
    }

    /// <summary>
    /// Sets Y value to the Vector2.
    /// </summary>
    /// <param name="v">Destination Vector2.</param>
    /// <param name="y">Y value.</param>
    public static Vector2 SetY(this Vector2 v, float y)
    {
        v.y = y;
        return v;
    }

    public static Vector2Int AddToInt(this Vector2 v, Vector2Int o)
    {
        return new Vector2Int((int)(v.x + o.x), (int)(v.y + o.y));
    }

    public static Vector3Int AddToInt(this Vector2 v, Vector3Int o)
    {
        return new Vector3Int((int)(v.x + o.x), (int)(v.y + o.y), (int)(o.z));
    }

    public static Vector3 Add(this Vector2 v, Vector3 o)
    {
        return new Vector3(v.x + o.x, v.y + o.y, o.z);
    }

    public static Vector3 Add(this Vector2 v, Vector3Int o)
    {
        return new Vector3(v.x + o.x, v.y + o.y, o.z);
    }

    public static Vector2 Add(this Vector2 v, float o)
    {
        return new Vector2(v.x + o, v.y + o);
    }





    public static Vector2Int MinusToInt(this Vector2 v, Vector2Int o)
    {
        return new Vector2Int((int)(v.x - o.x), (int)(v.y - o.y));
    }

    public static Vector3Int MinusToInt(this Vector2 v, Vector3Int o)
    {
        return new Vector3Int((int)(v.x - o.x), (int)(v.y - o.y), (int)(o.z));
    }

    public static Vector3 Minus(this Vector2 v, Vector3Int o)
    {
        return new Vector3(v.x - o.x, v.y - o.y, o.z);
    }

    public static Vector2 Minus(this Vector2 v, float o)
    {
        return new Vector2(v.x - o, v.y - o);
    }





    #region To
    public static Vector2Int ToInt(this Vector2 v)
    {
        return new Vector2Int((int)v.x , (int)v.y);
    }
    public static Vector3 ToVector3(this Vector2 v)
    {
        return new Vector3(v.x, v.y, 0);
    }
    #endregion

    #region Math
    public static Vector2 Rotate(this Vector2 v, float a)
    {
        return new Vector2(
            Mathf.Cos(a) * v.x - Mathf.Sin(a) * v.y,
            Mathf.Sin(a) * v.x + Mathf.Cos(a) * v.y);
    }
    #endregion


    public static Vector2Int MultiplyToInt(this Vector2 v, Vector2Int o)
    {
        return new Vector2Int((int)(v.x * o.x), (int)(v.y * o.y));
    }

    public static Vector3Int MultiplyToInt(this Vector2 v, Vector3Int o)
    {
        return new Vector3Int((int)(v.x * o.x), (int)(v.y * o.y), (int)(o.z));
    }

    public static Vector3 Multiply(this Vector2 v, Vector3Int o)
    {
        return new Vector3(v.x * o.x, v.y * o.y, o.z);
    }


    public static float Random(this Vector2 v)
    {
        return RandomEx.Range(v.x, v.y);
    }
}

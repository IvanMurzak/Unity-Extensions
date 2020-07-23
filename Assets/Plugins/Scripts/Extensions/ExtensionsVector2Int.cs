using UnityEngine;


/// <summary>
/// Extension methods for UnityEngine.Vector2Int.
/// </summary>
public static class ExtensionsVector2Int
{
    /// <summary>
    /// Sets X value to the Vector2Int.
    /// </summary>
    /// <param name="v">Destination Vector2Int.</param>
    /// <param name="x">X value.</param>
    public static Vector2Int SetX(this Vector2Int v, int x)
    {
        v.x = x;
        return v;
    }

    /// <summary>
    /// Sets Y value to the Vector2Int.
    /// </summary>
    /// <param name="v">Destination Vector2Int.</param>
    /// <param name="y">Y value.</param>
    public static Vector2Int SetY(this Vector2Int v, int y)
    {
        v.y = y;
        return v;
    }

    public static Vector2Int AddToInt(this Vector2Int v, Vector2 o)
    {
        return new Vector2Int((int)(v.x + o.x), (int)(v.y + o.y));
    }

    public static Vector3Int AddToInt(this Vector2Int v, Vector3Int o)
    {
        return new Vector3Int((int)(v.x + o.x), (int)(v.y + o.y), (int)(o.z));
    }

    public static Vector3 Add(this Vector2Int v, Vector3 o)
    {
        return new Vector3(v.x + o.x, v.y + o.y, o.z);
    }



    public static Vector2Int MultiplyToInt(this Vector2Int v, Vector2 o)
    {
        return new Vector2Int((int)(v.x * o.x), (int)(v.y * o.y));
    }

    public static Vector2 Multiply(this Vector2Int v, Vector2 o)
    {
        return new Vector2(v.x * o.x, v.y * o.y);
    }

    public static Vector3Int MultiplyToInt(this Vector2Int v, Vector3Int o)
    {
        return new Vector3Int((int)(v.x * o.x), (int)(v.y * o.y), (int)(o.z));
    }

    public static Vector3 Multiply(this Vector2Int v, Vector3 o)
    {
        return new Vector3(v.x * o.x, v.y * o.y, o.z);
    }

	public static float Random(this Vector2Int v)
	{
		return UnityEngine.Random.Range(v.x, v.y);
	}
}
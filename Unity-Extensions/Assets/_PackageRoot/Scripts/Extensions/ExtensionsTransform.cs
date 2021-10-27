using System;
using UnityEngine;


/// <summary>
/// Extension methods for UnityEngine.Transform.
/// </summary>
public static class ExtensionsTransform
{
    /// <summary>
    /// Sets transform.position.x.
    /// </summary>
    /// <param name="transform">Transform.</param>
    /// <param name="x">X value.</param>
    public static Transform SetPositionX(this Transform transform, float x)
    {
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
        return transform;
    }

    /// <summary>
    /// Sets transform.position.y.
    /// </summary>
    /// <param name="transform">Transform.</param>
    /// <param name="y">Y value.</param>
    public static Transform SetPositionY(this Transform transform, float y)
    {
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
        return transform;
    }

    /// <summary>
    /// Sets transform.position.z.
    /// </summary>
    /// <param name="transform">Transform.</param>
    /// <param name="z">Z value.</param>
    public static Transform SetPositionZ(this Transform transform, float z)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
        return transform;
    }

    /// <summary>
    /// Sets transform.localPosition.x.
    /// </summary>
    /// <param name="transform">Transform.</param>
    /// <param name="x">X value.</param>
    public static Transform SetLocalPositionX(this Transform transform, float x)
    {
        transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
        return transform;
    }

    /// <summary>
    /// Sets transform.localPosition.y.
    /// </summary>
    /// <param name="transform">Transform.</param>
    /// <param name="y">Y value.</param>
    public static Transform SetLocalPositionY(this Transform transform, float y)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
        return transform;
    }

    /// <summary>
    /// Sets transform.localPosition.z.
    /// </summary>
    /// <param name="transform">Transform.</param>
    /// <param name="z">Z value.</param>
    public static Transform SetLocalPositionZ(this Transform transform, float z)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
        return transform;
    }


	/// <summary>
	/// Sets transform.localScale.x.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="x">X value.</param>
	public static Transform SetLocalScaleX(this Transform transform, float x)
	{
		transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
		return transform;
	}

	/// <summary>
	/// Sets transform.localScale.y.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="y">Y value.</param>
	public static Transform SetLocalScaleY(this Transform transform, float y)
	{
		transform.localScale = new Vector3(transform.localScale.x, y, transform.localScale.z);
		return transform;
	}

	/// <summary>
	/// Sets transform.localScale.z.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="z">Z value.</param>
	public static Transform SetLocalScaleZ(this Transform transform, float z)
	{
		transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, z);
		return transform;
	}

	/// <summary>
	/// Makes the given game objects children of the transform.
	/// </summary>
	/// <param name="transform">Parent transform.</param>
	/// <param name="children">Game objects to make children.</param>
	public static Transform AddChildren(this Transform transform, GameObject[] children)
    {
        Array.ForEach(children, child => child.transform.parent = transform);
        return transform;
    }

    /// <summary>
    /// Makes the game objects of given components children of the transform.
    /// </summary>
    /// <param name="transform">Parent transform.</param>
    /// <param name="children">Components of game objects to make children.</param>
    public static Transform AddChildren(this Transform transform, Component[] children)
    {
        Array.ForEach(children, child => child.transform.parent = transform);
        return transform;
    }


    public static Transform Reset(this Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.identity;
        return transform;
    }

    public static Transform ResetPosition(this Transform transform)
    {
        transform.localPosition = Vector3.zero;
        return transform;
    }
    public static Transform ResetRotation(this Transform transform)
    {
        transform.localRotation = Quaternion.identity;
        return transform;
    }
    public static Transform ResetScale(this Transform transform)
    {
        transform.localScale = Vector3.one;
        return transform;
    }

    /// <summary>
    /// Sets the position of a transform's children to zero.
    /// </summary>
    /// <param name="transform">Parent transform.</param>
    /// <param name="recursive">Also reset ancestor positions?</param>
    public static Transform ResetChildPositions(this Transform transform, bool recursive = false)
    {
        foreach (Transform child in transform)
        {
            child.position = Vector3.zero;

            if (recursive)
            {
                child.ResetChildPositions(recursive);
            }
        }
        return transform;
    }

    /// <summary>
    /// Sets the layer of the transform's children.
    /// </summary>
    /// <param name="transform">Parent transform.</param>
    /// <param name="layerName">Name of layer.</param>
    /// <param name="recursive">Also set ancestor layers?</param>
    public static Transform SetChildLayers(this Transform transform, string layerName, bool recursive = false)
    {
        var layer = LayerMask.NameToLayer(layerName);
        SetChildLayersHelper(transform, layer, recursive);
        return transform;
    }

    public static Transform MoveToHell(this Transform transform)
    {
        transform.position = Vector3.one * 100000;
        return transform;
    }

    public static void LookAt2D(this Transform transform, Vector3 target)
    {
        var dir = target - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public static Transform DestroyChildren(this Transform transform)
    {
        for(int i = 0; i < transform.childCount; i++)
            GameObject.Destroy(transform.GetChild(i).gameObject);
        return transform;
    }

    public static Transform DestroyChildrenImmediate(this Transform transform)
    {
        while (transform.childCount > 0)
            GameObject.DestroyImmediate(transform.GetChild(0).gameObject);
        return transform;
    }

    static void SetChildLayersHelper(Transform transform, int layer, bool recursive)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.layer = layer;

            if (recursive)
            {
                SetChildLayersHelper(child, layer, recursive);
            }
        }
    }
}

using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ExtensionsComponent
{
	public static bool IsWorldSpace(this Component original)
	{
		var canvas			= original == null ? null : original.GetComponentInParent<Canvas>();
		var isWorldSpace	= original != null && (canvas == null || canvas.renderMode == RenderMode.WorldSpace);
		return isWorldSpace;
	}
    public static T GetOrAddComponent<T>(this Component original) where T : Component
    {
        T component = original.gameObject.GetComponent<T>();
        if (component == null) component = original.gameObject.AddComponent<T>();
        return component;
    }
    public static T CopyComponent<T>(this T original, GameObject destination) where T : Component
    {
        System.Type type = original.GetType();
        Component copy = destination.AddComponent(type);
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        return copy as T;
    }

    public static string FullName(this Component original)
    {
        return FullName(original, Path.DirectorySeparatorChar);
    }

    public static string FullName(this Component original, char separator)
    {
        if (original.transform.parent != null)
            return FullName(original.transform.parent, separator) + separator + original.name;

        return SceneManager.GetActiveScene().name + separator + original.transform.name;
    }
}
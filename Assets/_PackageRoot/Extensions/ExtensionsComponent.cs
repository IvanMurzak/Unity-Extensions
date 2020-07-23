using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ExtensionsComponent
{
	public static bool IsWorldSpace(this Component compoennt)
	{
		var canvas			= compoennt == null ? null : compoennt.GetComponentInParent<Canvas>();
		var isWorldSpace	= compoennt != null && (canvas == null || canvas.renderMode == RenderMode.WorldSpace);
		return isWorldSpace;
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
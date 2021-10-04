using UnityEngine;
using System;

public static class ExtensionsGameObject
{
    public static string SceneName()
    {
#if UNITY_5_2 || UNITY_5_1 || UNITY_5_0
        return Application.loadedLevelName;
#else
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
#endif
    }

    private static string StringPattern(GameObject gameObject, string message)
    {
        return "[" + SceneName() + "]:" + gameObject.name + ":" + message;
    }

    public static void LogError(this GameObject gameObject, string message)
    {
        Debug.LogErrorFormat(gameObject, StringPattern(gameObject, message));
    }

    public static void Log(this GameObject gameObject, string message)
    {
        Debug.LogFormat(gameObject, StringPattern(gameObject, message));
    }

    public static T AddComponent<T>(this GameObject gameObject, Action<T> action) where T : Component
    {
        using (gameObject.Deactivate())
        {
            T component = gameObject.AddComponent<T>();
            if (action != null) action(component);
            return component;
        }
    }

    public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
    {
        T component = gameObject.GetComponent<T>();
        if (component == null) component = gameObject.AddComponent<T>();
        return component;
    }
    public static void SetLayer(this GameObject obj, string newLayer) => SetLayer(obj, LayerMask.NameToLayer(newLayer));
    public static void SetLayer(this GameObject obj, int newLayer)
    {
        if (null == obj) return;
        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
                continue;
            child.gameObject.layer = newLayer;
        }
    }
    public static void SetLayerRecursively(this GameObject obj, string newLayer) => SetLayerRecursively(obj, LayerMask.NameToLayer(newLayer));
    public static void SetLayerRecursively(this GameObject obj, int newLayer)
    {
        if (null == obj) return;
        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
                continue;
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    public static IDisposable Deactivate(this GameObject gameObject)
    {
        return new GameObjectDeactivateSection(gameObject);
    }

    private class GameObjectDeactivateSection : IDisposable
    {
        GameObject go;
        bool oldState;
        public GameObjectDeactivateSection(GameObject gameObject)
        {
            go = gameObject;
            oldState = go.activeSelf;
            go.SetActive(false);
        }
        public void Dispose()
        {
            go.SetActive(oldState);
        }
    }
}
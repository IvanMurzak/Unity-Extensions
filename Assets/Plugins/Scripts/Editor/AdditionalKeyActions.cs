// #if UNITY_EDITOR

using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class AdditionalKeyActions
{
    [MenuItem("Tools/Additional Key Actions/Toogle IsActive %e")]
    private static void ToogleGameObjectActive()
    {
        foreach(var gameObject in Selection.gameObjects)
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
        EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
    }
}

// #endif

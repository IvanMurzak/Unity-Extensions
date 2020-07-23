using UnityEngine;

public static class PrefabUtils
{
	public static void EditorApplyPrefabChanges(Component component)
	{
#if UNITY_EDITOR
		if (UnityEditor.PrefabUtility.IsPartOfPrefabInstance(component))
		{
			Sirenix.OdinInspector.Editor.OdinPrefabUtility.UpdatePrefabInstancePropertyModifications(component, false);
		}
#endif
	}
}
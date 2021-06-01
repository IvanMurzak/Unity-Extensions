using UnityEngine;

public static class ExtensionComponentUI
{
	public static RectTransform ParentRectTransform(this Component component) => component.transform.parent.GetComponent<RectTransform>();
}

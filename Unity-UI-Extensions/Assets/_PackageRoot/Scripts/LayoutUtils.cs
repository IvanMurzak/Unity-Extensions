using UnityEngine;
using UnityEngine.UI;

public static class LayoutUtils
{
	public static void ForceRebuilLayout(GameObject label)
	{
		if (label == null) return;

		var rectTransform			= label.GetComponent<RectTransform>();
		var parentRectTransform		= label.transform.parent.GetComponent<RectTransform>();

		if (rectTransform)			LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
		if (parentRectTransform)	LayoutRebuilder.ForceRebuildLayoutImmediate(parentRectTransform);
	}
	public static void ForceRebuilLayout(RectTransform label)
	{
		if (label == null) return;

		var rectTransform			= label;
		var parentRectTransform		= label.parent.GetComponent<RectTransform>();

		if (rectTransform)			LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
		if (parentRectTransform)	LayoutRebuilder.ForceRebuildLayoutImmediate(parentRectTransform);
	}
	public static void ForceRebuilLayout(RectTransform rectTransform, RectTransform parentRectTransform)
	{
		if (rectTransform == null)	return;

		if (rectTransform)			LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
		if (parentRectTransform)	LayoutRebuilder.ForceRebuildLayoutImmediate(parentRectTransform);
	}
}
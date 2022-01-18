using UnityEngine;
using System.Collections;

public static class GizmosHelper
{
    public static void DrawPathGradient(Vector3[] vertices, Color from, Color to, bool closed = false)
    {
        if (vertices != null && vertices.Length > 0)
        {
            for (int i = 1; i < vertices.Length; i++)
            {
                var p = (float)i / vertices.Length;                
                Gizmos.color = from.Between(to, p);
                Gizmos.DrawLine(vertices[i - 1], vertices[i]);
            }

            if (closed)
            {
                Gizmos.color = to;
                Gizmos.DrawLine(vertices[vertices.Length - 1], vertices[0]);
            }
        }
    }

    public static void DrawPath(Vector3[] vertices, Color color, bool closed = false)
    {
        if (vertices != null && vertices.Length > 0)
        {
            Gizmos.color = color;
            for (int i = 1; i < vertices.Length; i++)
                Gizmos.DrawLine(vertices[i - 1], vertices[i]);

            if (closed) Gizmos.DrawLine(vertices[vertices.Length - 1], vertices[0]);
        }
    }

    public static void DrawBounds(Bounds bounds, Color color)
    {
        DrawPath(new Vector3[]
        {
            bounds.min,
            new Vector3(bounds.max.x, bounds.min.y),
            bounds.max,
            new Vector3(bounds.min.x, bounds.max.y)
        }, color, true);
    }

    public static void DrawX(Vector3 point, float radius)
    {
		DrawX(point, radius, Gizmos.color);
	}
	public static void DrawX(Vector3 point, float radius, Color color)
	{
		var prevColor = Gizmos.color;
		Gizmos.color = color;

		Gizmos.DrawLine(point - Vector3.up * radius, point + Vector3.up * radius);
		Gizmos.DrawLine(point - Vector3.left * radius, point + Vector3.left * radius);
		Gizmos.DrawLine(point - Vector3.forward * radius, point + Vector3.forward * radius);

		Gizmos.color = prevColor;
	}
	public static void DrawString(string text, Vector3 worldPos, int fontSize = 14, Color? colour = null)
	{
#if UNITY_EDITOR
		UnityEditor.Handles.BeginGUI();
        GUI.skin.label.fontSize = fontSize;
		if (colour.HasValue) GUI.color = colour.Value;
		var view        = UnityEditor.SceneView.currentDrawingSceneView;
		var screenPos   = view.camera.WorldToScreenPoint(worldPos);
        var size        = GUI.skin.label.CalcSize(new GUIContent(text));
		GUI.Label(new Rect(screenPos.x - (size.x / 2), -screenPos.y + view.position.height + 4, size.x, size.y), text);
		UnityEditor.Handles.EndGUI();
#endif
	}
}
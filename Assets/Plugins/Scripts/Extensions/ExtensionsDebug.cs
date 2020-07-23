using System;
using System.Reflection;
using System.ComponentModel;
using UnityEngine;

public static class DebugUtils
{
    public static void DrawX(Vector3 position, float radius, Color color)
    {
        Vector3 from1 = position - Vector3.up * radius;
        Vector3 to1 = position + Vector3.up * radius;

        Vector3 from2 = position - Vector3.right * radius;
        Vector3 to2 = position + Vector3.right * radius;
        
        Debug.DrawLine(from1, to1, color);
        Debug.DrawLine(from2, to2, color);
    }
}

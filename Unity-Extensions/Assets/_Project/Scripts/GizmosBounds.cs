using UnityEngine;

public class GizmosBounds : MonoBehaviour
{
    public Vector3 center, size;
    public Color color = Color.red;
    private void OnDrawGizmosSelected()
    {
        GizmosHelper.DrawBounds(new Bounds(center, size), color);
    }
}
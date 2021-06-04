using UnityEngine;
using Shapes;

[ExecuteInEditMode]
[AddComponentMenu("Shapes/TriangleRectTransform")]
[RequireComponent(typeof(Triangle))]
public class TriangleRectTransform : UIBehaviourShape<Triangle>
{
    [Header("Mode works only if 'Save Aspect Ratio' is True", order = 1)]
    [Header("", order = 2)]
    public Mode mode;
    public bool saveAspectRatio = true;

    public override void Execute(Triangle triangle, RectTransform rectTransform)
    {
        var rtPivot = rectTransform.pivot;
        var rtSize = rectTransform.rect.size;
        var rtCenter = rtPivot * rtSize;

        var bounds = triangle.GetBounds();
        var scaleBy = new Vector2
        (
            bounds.size.x == 0 ? 0 : rtSize.x / bounds.size.x,
            bounds.size.y == 0 ? 0 : rtSize.y / bounds.size.y
        );

        if (saveAspectRatio && scaleBy.x != scaleBy.y)
            scaleBy = CorrectRatio(scaleBy, bounds, mode);

        Vector3 pivotOffset = rtCenter - rtSize / 2;

        triangle.A = CorrectPoint(triangle.A, scaleBy, bounds, pivotOffset);
        triangle.B = CorrectPoint(triangle.B, scaleBy, bounds, pivotOffset);
        triangle.C = CorrectPoint(triangle.C, scaleBy, bounds, pivotOffset);

        triangle.meshOutOfDate = true;
    }

    protected virtual Vector3 CorrectPoint(Vector3 point, Vector3 scaleBy, Bounds bounds, Vector3 pivotOffset)
    {
        return Vector3.Scale(point, scaleBy) - bounds.center - pivotOffset;
    }
}
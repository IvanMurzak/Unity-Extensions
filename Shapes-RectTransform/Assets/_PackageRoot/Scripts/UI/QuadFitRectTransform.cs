using UnityEngine;
using Shapes;

[ExecuteAlways]
[AddComponentMenu("Shapes/QuadRectTransform")]
[RequireComponent(typeof(Quad))]
public class QuadFitRectTransform : UIBehaviourShape<Quad>
{
    [Header("Mode works only if 'Save Aspect Ratio' is True", order = 1)]
    [Header("", order = 2)]
    public Mode mode;
    public bool saveAspectRatio = true;

    public override void Execute(Quad quad, RectTransform rectTransform)
    {
        var rtPivot = rectTransform.pivot;
        var rtSize = rectTransform.rect.size;
        var rtCenter = rtPivot * rtSize;

        var bounds = quad.GetBounds();
        if (bounds.size.x <= 0 ||
            bounds.size.y <= 0 ||
            bounds.size.x == float.NaN ||
            bounds.size.y == float.NaN ||
            rtSize.x < 0 ||
            rtSize.y < 0)
            return;

        var scaleBy = new Vector2
        (
            rtSize.x == 0 ? ScaleByWhenRectSizeIsZero : rtSize.x / bounds.size.x,
            rtSize.y == 0 ? ScaleByWhenRectSizeIsZero : rtSize.y / bounds.size.y
        );

        if (saveAspectRatio && scaleBy.x != scaleBy.y)
            scaleBy = CorrectRatio(scaleBy, bounds, mode);

        Vector3 pivotOffset = rtCenter - rtSize / 2;

        quad.A = CorrectPoint(quad.A, scaleBy, bounds, pivotOffset);
        quad.B = CorrectPoint(quad.B, scaleBy, bounds, pivotOffset);
        quad.C = CorrectPoint(quad.C, scaleBy, bounds, pivotOffset);
        quad.D = CorrectPoint(quad.D, scaleBy, bounds, pivotOffset);

        quad.meshOutOfDate = true;
    }

    protected virtual Vector3 CorrectPoint(Vector3 point, Vector3 scaleBy, Bounds bounds, Vector3 pivotOffset)
	{
        return Vector3.Scale(point, scaleBy) - bounds.center - pivotOffset;
    }
}
using UnityEngine;
using Shapes;

[ExecuteInEditMode]
[AddComponentMenu("Shapes/PolylineRectTransform")]
[RequireComponent(typeof(Polyline))]
public class PolylineRectTransform : UIBehaviourShape<Polyline>
{
    [Header("Mode works only if 'Save Aspect Ratio' is True", order = 1)]
    [Header("", order = 2)]
    public Mode mode;
    public bool saveAspectRatio = true;
    public bool ignoreThickness = true;

    public override void Execute(Polyline polyline, RectTransform rectTransform)
    {
        var rtPivot = rectTransform.pivot;
        var rtSize = rectTransform.rect.size;
        var rtCenter = rtPivot * rtSize;

        var bounds = ignoreThickness ? polyline.GetBounds() : GetBoundsWithThickness(polyline);
        var scaleBy = new Vector2
        (
            bounds.size.x == 0 ? 0 : rtSize.x / bounds.size.x,
            bounds.size.y == 0 ? 0 : rtSize.y / bounds.size.y
        );

        if (saveAspectRatio && scaleBy.x != scaleBy.y)
            scaleBy = CorrectRatio(scaleBy, bounds, mode);

        Vector3 pivotOffset = rtCenter - rtSize / 2;

        for (int i = 0; i < polyline.points.Count; i++)
        {
            var point = Vector3.Scale(polyline.points[i].point, scaleBy) - bounds.center - pivotOffset;
            polyline.SetPointPosition(i, point);
        }

        polyline.meshOutOfDate = true;
    }

    protected virtual Bounds GetBoundsWithThickness(Polyline polyline)
    {
        if (polyline.points.Count < 2)
            return default;
        Vector3 min = Vector3.one * float.MaxValue;
        Vector3 max = Vector3.one * float.MinValue;
        foreach (var pt in polyline.points)
        {
            min = Vector3.Min(min, pt.point - Vector3.one * polyline.Thickness * pt.thickness);
            max = Vector3.Max(max, pt.point + Vector3.one * polyline.Thickness * pt.thickness);
        }

        if (polyline.Geometry == PolylineGeometry.Flat2D)
            min.z = max.z = 0;

        return new Bounds((max + min) * 0.5f, max - min);
    }
}
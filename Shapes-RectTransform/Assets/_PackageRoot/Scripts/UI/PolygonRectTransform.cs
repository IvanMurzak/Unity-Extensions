using UnityEngine;
using Shapes;

[ExecuteAlways]
[AddComponentMenu("Shapes/PolygonRectTransform")]
[RequireComponent(typeof(Polygon))]
public class PolygonRectTransform : UIBehaviourShape<Polygon>
{
    [Header("Mode works only if 'Save Aspect Ratio' is True", order = 1)]
    [Header("", order = 2)]
    public Mode mode;
    public bool saveAspectRatio = true;

    public override void Execute(Polygon polygon, RectTransform rectTransform)
    {
        var rtPivot = rectTransform.pivot;
        var rtSize = rectTransform.rect.size;
        var rtCenter = rtPivot * rtSize;

        var bounds = polygon.GetBounds();
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

        for (int i = 0; i < polygon.points.Count; i++)
        {
            var point = Vector3.Scale(polygon.points[i], scaleBy) - bounds.center - pivotOffset;
            polygon.SetPointPosition(i, point);
        }

        polygon.meshOutOfDate = true;
    }
}
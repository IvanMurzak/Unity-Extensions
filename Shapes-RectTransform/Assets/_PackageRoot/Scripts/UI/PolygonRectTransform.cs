using UnityEngine;
using Shapes;

[ExecuteInEditMode]
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
        var scaleBy = new Vector2
        (
            bounds.size.x == 0 ? 0 : rtSize.x / bounds.size.x,
            bounds.size.y == 0 ? 0 : rtSize.y / bounds.size.y
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
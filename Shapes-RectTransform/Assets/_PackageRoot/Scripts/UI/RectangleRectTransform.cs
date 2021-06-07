using UnityEngine;
using Shapes;

[ExecuteAlways]
[AddComponentMenu("Shapes/RectangleRectTransform")]
public class RectangleRectTransform : UIBehaviourShapeNested<Rectangle>
{
    [Header("'Rectangle' component has to be a children!", order = 0)]
    [Header("Mode works only if 'Save Aspect Ratio' is True", order = 1)]
    [Header("", order = 2)]
    public Mode mode;
    public bool saveAspectRatio = true;

    public override void Execute(Rectangle rectangle, RectTransform rectTransform)
    {
        var rtPivot = rectTransform.pivot;
        var rtSize = rectTransform.rect.size;
        var rtCenter = rtPivot * rtSize;

        var bounds = rectangle.GetBounds();
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

        rectangle.Pivot = RectPivot.Center;
        rectangle.transform.localPosition = -(rtCenter - rtSize / 2);

        if (saveAspectRatio && scaleBy.x != scaleBy.y)
        {
            scaleBy = CorrectRatio(scaleBy, bounds, mode);

            rectangle.Width = bounds.size.x * scaleBy.x;
            rectangle.Height = bounds.size.y * scaleBy.y;
        }
        else
        {
            rectangle.Width = bounds.size.x * scaleBy.x;
            rectangle.Height = bounds.size.y * scaleBy.y;
        }

        rectangle.meshOutOfDate = true;
    }
}
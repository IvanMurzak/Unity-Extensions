using UnityEngine;
using Shapes;

[ExecuteInEditMode]
[AddComponentMenu("Shapes/RectangleRectTransform")]
public class RectangleRectTransform : UIBehaviourShape<Rectangle>
{
    [Header("'Rectangle' component has to be a children!", order = 0)]
    [Header("Mode works only if 'Save Aspect Ratio' is True", order = 1)]
    [Header("", order = 2)]
    public Mode mode;
    public bool saveAspectRatio = true;

    protected override Rectangle GetTargetComponent() => GetComponentInChildren<Rectangle>();

    public override void Execute(Rectangle rectangle, RectTransform rectTransform)
    {
        var rtPivot = rectTransform.pivot;
        var rtSize = rectTransform.rect.size;
        var rtCenter = rtPivot * rtSize;

        var bounds = rectangle.GetBounds();
        var scaleBy = new Vector2
        (
            bounds.size.x == 0 ? 0 : rtSize.x / bounds.size.x,
            bounds.size.y == 0 ? 0 : rtSize.y / bounds.size.y
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
            rectangle.Width = rtSize.x;
            rectangle.Height = rtSize.y;
        }

        rectangle.meshOutOfDate = true;
    }
}
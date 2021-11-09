using UnityEngine;
using Shapes;

[ExecuteAlways]
[AddComponentMenu("Shapes/DiscRectTransform")]
public class DiscRectTransform : UIBehaviourShapeNested<Disc>
{
    [Header("'Rectangle' component has to be a children!", order = 0)]
    [Header("", order = 2)]
    public Mode mode;

    public override void Execute(Disc disc, RectTransform rectTransform)
    {
        var rtPivot = rectTransform.pivot;
        var rtSize = rectTransform.rect.size;
        var rtCenter = rtPivot * rtSize;
        var discThickness = disc.ThicknessSpace == ThicknessSpace.Pixels ? disc.Thickness : 0f;

        disc.transform.localPosition = -(rtCenter - rtSize / 2);

        disc.RadiusSpace = ThicknessSpace.Pixels;

        if (mode == Mode.Fill) disc.Radius = (Mathf.Max(rtSize.x, rtSize.y) - discThickness) / 2f;
        if (mode == Mode.Fit) disc.Radius = (Mathf.Min(rtSize.x, rtSize.y) - discThickness) / 2f;
    }
}

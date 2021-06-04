using UnityEngine;
using Shapes;

[ExecuteInEditMode]
[AddComponentMenu("Shapes/RegularPolygonRectTransform")]
public class RegularPolygonRectTransform : UIBehaviourShape<RegularPolygon>
{
    [Header("'Rectangle' component has to be a children!", order = 0)]
    [Header("", order = 2)]
    public Mode mode;

    protected override RegularPolygon GetTargetComponent() => GetComponentInChildren<RegularPolygon>();

    public override void Execute(RegularPolygon regularPolygon, RectTransform rectTransform)
    {
        var rtPivot = rectTransform.pivot;
        var rtSize = rectTransform.rect.size;
        var rtCenter = rtPivot * rtSize;

        regularPolygon.transform.localPosition = -(rtCenter - rtSize / 2);
        regularPolygon.RadiusSpace = ThicknessSpace.Meters;

        if (mode == Mode.Fill) regularPolygon.Radius = Mathf.Max(rtSize.x, rtSize.y) / 2;
        if (mode == Mode.Fit) regularPolygon.Radius = Mathf.Min(rtSize.x, rtSize.y) / 2;
    }
}
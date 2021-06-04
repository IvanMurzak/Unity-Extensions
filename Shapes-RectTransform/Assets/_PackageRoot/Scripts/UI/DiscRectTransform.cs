using UnityEngine;
using Shapes;

[ExecuteInEditMode]
[AddComponentMenu("Shapes/DiscRectTransform")]
public class DiscRectTransform : UIBehaviourShape<Disc>
{
    [Header("'Rectangle' component has to be a children!", order = 0)]
    [Header("", order = 2)]
    public Mode mode;

    protected override Disc GetTargetComponent() => GetComponentInChildren<Disc>();

    public override void Execute(Disc disc, RectTransform rectTransform)
    {
        var rtPivot = rectTransform.pivot;
        var rtSize = rectTransform.rect.size;
        var rtCenter = rtPivot * rtSize;

        disc.transform.localPosition = -(rtCenter - rtSize / 2);

        disc.RadiusSpace = ThicknessSpace.Meters;

        if (mode == Mode.Fill) disc.Radius = Mathf.Max(rtSize.x, rtSize.y) / 2;
        if (mode == Mode.Fit) disc.Radius = Mathf.Min(rtSize.x, rtSize.y) / 2;
    }
}
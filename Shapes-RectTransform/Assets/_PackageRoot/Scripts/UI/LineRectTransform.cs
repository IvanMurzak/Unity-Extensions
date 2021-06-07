using UnityEngine;
using Shapes;

[ExecuteAlways]
[AddComponentMenu("Shapes/LineRectTransform")]
[RequireComponent(typeof(Line))]
public class LineRectTransform : UIBehaviourShape<Line>
{
    public Mode mode;

    public override void Execute(Line line, RectTransform rectTransform)
    {
        var rtPivot = rectTransform.pivot;
        var rtSize = rectTransform.rect.size;

        line.ThicknessSpace = ThicknessSpace.Meters;
        line.Thickness = rtSize.y;

        if (line.EndCaps == LineEndCap.Round && mode == Mode.Fit)
        {
            line.Start = new Vector2
            (
                -rtSize.x * rtPivot.x + rtSize.y / 2, 
                rtSize.y / 2 - rtPivot.y * rtSize.y
            );
            line.End = new Vector2
            (
                Mathf.Max(line.Start.x, rtSize.x * (1f - rtPivot.x) - rtSize.y / 2), 
                rtSize.y / 2 - rtPivot.y * rtSize.y
            );
        }
        else
        {
            line.Start = new Vector2
            (
                -rtSize.x * rtPivot.x, 
                rtSize.y / 2 - rtPivot.y * rtSize.y
            );
            line.End = new Vector2
            (
                rtSize.x * (1f - rtPivot.x), 
                rtSize.y / 2 - rtPivot.y * rtSize.y
            );
        }
    }
}
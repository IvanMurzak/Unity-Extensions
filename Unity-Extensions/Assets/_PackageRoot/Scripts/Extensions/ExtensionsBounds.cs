using UnityEngine;

public static class ExtensionsBounds
{
    public static BoundsInt ToInt(this Bounds v) => new BoundsInt(v.center.ToInt(), v.size.ToInt());
}

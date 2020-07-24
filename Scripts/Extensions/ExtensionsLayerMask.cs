using UnityEngine;

public static class ExtensionsLayerMask
{
    public static bool CheckLayermask(this LayerMask layerMask, GameObject other)
    {
        return CheckLayermask(layerMask, other.layer);
    }

    public static bool CheckLayermask(this LayerMask layerMask, int layer)
    {
        return ((1 << layer) & layerMask) != 0;
    }
}

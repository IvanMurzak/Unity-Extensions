using UnityEngine;

public static class ExtensionsColor
{
	public static Color SetR(this Color c, float r)
    {
        c.r = r;
        return c;
    }
    public static Color SetG(this Color c, float g)
    {
        c.g = g;
        return c;
    }
    public static Color SetB(this Color c, float b)
    {
        c.b = b;
        return c;
    }
    public static Color SetA(this Color c, float a)
    {
        c.a = a;
        return c;
    }

    public static Color Parse(this Color c, string hex)
    {
        ColorUtility.TryParseHtmlString(hex, out c);
        return c;
    }

    public static Color Between(this Color from, Color to, float percent)
    {
        return new Color
        (
            from.r + (to.r - from.r) * percent, 
            from.g + (to.g - from.g) * percent, 
            from.b + (to.b - from.b) * percent, 
            from.a + (to.a - from.a) * percent
        );
    }
}

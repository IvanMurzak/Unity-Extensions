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

    public static bool TryParseColor(this string hex, out Color c)
    {
        if (!hex.StartsWith("#")) hex = "#" + hex;
        return ColorUtility.TryParseHtmlString(hex, out c);
    }

    public static Color Parse(this string hex, Color defaultColor)
    {
        var c = new Color();
        if (!hex.StartsWith("#")) hex = "#" + hex;
        if (ColorUtility.TryParseHtmlString(hex, out c))
		{
            return c;
		}
        else
		{
            c.r = defaultColor.r;
            c.g = defaultColor.g;
            c.b = defaultColor.b;
            c.a = defaultColor.a;
            return defaultColor;
        }
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
    public static string ToHexRGBA(this Color c, bool addHash = false)
    {
        var hex = ColorUtility.ToHtmlStringRGBA(c);
        if (addHash) return "#" + hex;
        return hex;
    }
    public static string ToHexRGB(this Color c, bool addHash = false)
    {
        var hex = ColorUtility.ToHtmlStringRGB(c);
        if (addHash) return "#" + hex;
        return hex;
    }
}

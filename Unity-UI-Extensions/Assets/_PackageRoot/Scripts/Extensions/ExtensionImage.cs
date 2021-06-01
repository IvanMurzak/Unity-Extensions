using UnityEngine;
using UnityEngine.UI;

public static class ExtensionImage
{
	public static float Alpha(this Image image) => image.color.a;
	public static Image SetAlpha(this Image image, float alpha)
	{
		image.color = image.color.SetA(alpha);
		return image;
    }

    public static Image FillInto(this Image image) => FillInto(image, Vector2.zero);
    public static Image FillInto(this Image image, Vector2 oversize, RectTransform rectTransform = null, Sprite sprite = null)
    {
        float width;
        float height;

        rectTransform   = rectTransform ?? (RectTransform)image.rectTransform.parent;
        sprite          = sprite ?? image.sprite;

        var size = sprite.textureRect.size;
        if (size.y > size.x)
        {
            var reversedAspectRatio = size.y / size.x;
            width = rectTransform.rect.size.x + oversize.x * 2;
            height = width * reversedAspectRatio;
        }
        else
        {
            var aspectRatio = size.x / size.y;
            height = rectTransform.rect.size.y + oversize.y * 2;
            width = height * aspectRatio;
        }
        image.rectTransform.SetWidth(width);
        image.rectTransform.SetHeight(height);

        return image;
    }
    public static Image CorrectAspectRatioFromHeight(this Image image, Sprite sprite = null)
    {
        sprite = sprite ?? image.sprite;
        var aspectRatio = sprite.textureRect.size.x / sprite.textureRect.size.y;
        image.rectTransform.SetAspectRatioFromHeight(aspectRatio);
        return image;
    }
    public static Image CorrectAspectRatioFromWidth(this Image image, Sprite sprite = null)
    {
        sprite = sprite ?? image.sprite;
        var aspectRatio = sprite.textureRect.size.x / sprite.textureRect.size.y;
        image.rectTransform.SetAspectRatioFromWidth(aspectRatio);
        return image;
    }
}
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

        rectTransform       = rectTransform ?? (RectTransform)image.rectTransform.parent;
        sprite              = sprite ?? image.sprite;

        var size            = sprite.textureRect.size;
        var aspectRatio     = size.x / size.y;
        var aspectRatioRect = rectTransform.rect.size.x / rectTransform.rect.size.y;

        if (aspectRatio < aspectRatioRect)
        {
            width = rectTransform.rect.size.x + oversize.x * 2;
            height = width / aspectRatio;
        }
        else
        {
            height = rectTransform.rect.size.y + oversize.y * 2;
            width = height * aspectRatio;
        }
        image.rectTransform.SetWidth(width);
        image.rectTransform.SetHeight(height);

        return image;
    }
    public static Image FitInto(this Image image) => FitInto(image, Vector2.zero);
    public static Image FitInto(this Image image, Vector2 oversize, RectTransform rectTransform = null, Sprite sprite = null)
    {
        float width;
        float height;

        rectTransform       = rectTransform ?? (RectTransform)image.rectTransform.parent;
        sprite              = sprite ?? image.sprite;

        var size            = sprite.textureRect.size;
        var aspectRatio     = size.x / size.y;
        var aspectRatioRect = rectTransform.rect.size.x / rectTransform.rect.size.y;

        if (aspectRatio > aspectRatioRect)
        {
            width = rectTransform.rect.size.x + oversize.x * 2;
            height = width / aspectRatio;
        }
        else
        {
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
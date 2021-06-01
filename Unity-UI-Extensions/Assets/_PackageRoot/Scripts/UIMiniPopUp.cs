using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMiniPopUp : BaseMonoBehaviour
{
    [Required] public TextMeshProUGUI	text;
    [Required] public Graphic[]			sprites;

    public void SetFrameColor(Color color)
    {
        foreach (var sprite in sprites) sprite.color = color;
    }

    public void SetTextColor(Color color)
    {
        text.color = color;
    }

    public void SetTextFontSize(int fontSize)
    {
        text.fontSize = fontSize;
    }
}

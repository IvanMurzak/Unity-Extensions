using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class ImageFit_Test : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Vector2 oversize;

    void Update()
    {
        if (image)
        {
            image.FitInto(oversize);
        }
    }
}
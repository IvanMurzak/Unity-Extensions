using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class ImageFill_Test : MonoBehaviour
{
    [SerializeField] Image      image;
    [SerializeField] Vector2    oversize;

    void Update()
    {
        if (image)
        {
            image.FillInto(oversize);
        }
    }
}
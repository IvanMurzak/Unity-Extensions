using UnityEngine;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public abstract class UIBehaviourShape<T> : UIBehaviour where T : Component
{
    protected virtual T GetTargetComponent() => GetComponent<T>();

    private T target;
    protected T Target
    {
        get
        {
            if (target == null)
                target = GetTargetComponent();
            return target;
        }
    }
    private RectTransform rectTransform;
    private RectTransform RectTransform
    {
        get
        {
            if (rectTransform == null)
                rectTransform = GetComponent<RectTransform>();
            return rectTransform;
        }
    }
    protected override void Awake()
    {
        base.Awake();
        Execute(Target, RectTransform);
    }
#if UNITY_EDITOR
    protected override void OnValidate()
	{
		base.OnValidate();
        Execute(Target, RectTransform);
    }
#endif
    protected override void OnRectTransformDimensionsChange()
    {
        base.OnRectTransformDimensionsChange();
        Execute(Target, RectTransform);
    }

    protected virtual Vector2 CorrectRatio(Vector2 scaleBy, Bounds bounds, Mode mode)
    {
        var ratio = bounds.size.x / bounds.size.y;

        if (mode == Mode.Fill)
        {
            if (scaleBy.x < scaleBy.y)
                scaleBy.x = bounds.size.x / (bounds.size.y / scaleBy.y * ratio);
            else
                scaleBy.y = bounds.size.y / (bounds.size.x / scaleBy.x / ratio);

        }
        if (mode == Mode.Fit)
        {
            if (scaleBy.x < scaleBy.y)
                scaleBy.y = bounds.size.y / (bounds.size.x / scaleBy.x / ratio);
            else
                scaleBy.x = bounds.size.x / (bounds.size.y / scaleBy.y * ratio);
        }

        return scaleBy;
    }

    public abstract void Execute(T target, RectTransform rectTransform);
}
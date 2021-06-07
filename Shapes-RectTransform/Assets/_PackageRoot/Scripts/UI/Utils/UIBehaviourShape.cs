using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

[ExecuteAlways]
public abstract class UIBehaviourShape<T> : UIBehaviour where T : Component
{
    protected static float ScaleByWhenRectSizeIsZero = 1f;

    protected virtual T GetTargetComponent() => GetComponent<T>();

    private int lastFrameCount = -1;

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

    private IEnumerator ExecuteAfterFrame()
	{
        yield return new WaitForEndOfFrame();
        if (Target != null)
        {
            Execute(Target, RectTransform);
        }
    }
    private void ExecuteInternal()
    {
        if (!gameObject.activeInHierarchy)
            return;

        if (Time.frameCount != lastFrameCount)
        {
            lastFrameCount = Time.frameCount;
            if (Target != null)
            {
                Execute(Target, RectTransform);
            }
        }
        else
		{
            StartCoroutine(ExecuteAfterFrame());
		}
    }
#if UNITY_EDITOR
    protected override void OnValidate()
	{
		base.OnValidate();
        ExecuteInternal();
    }
#endif

	protected override void OnRectTransformDimensionsChange()
    {
        base.OnRectTransformDimensionsChange();
        ExecuteInternal();
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
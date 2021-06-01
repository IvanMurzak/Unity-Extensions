using UnityEngine;
using DG.Tweening;

public static class ExtensionDOTweenRectTransform
{
	public static Tween		DOTweenLocalResizeRight		(this RectTransform rt, float to,	float duration)	=> DOTween.To(() => rt.LocalRightX(),	rt.SetLocalRight,		to, duration);
	public static Tween		DOTweenLocalResizeBottom	(this RectTransform rt, float to,	float duration)	=> DOTween.To(() => rt.LocalBottomY(),	rt.SetLocalBottom,		to, duration);
	public static Tween		DOTweenLocalResizeLeft		(this RectTransform rt, float to,	float duration)	=> DOTween.To(() => rt.LocalLeftX(),	rt.SetLocalLeft,		to, duration);
	public static Tween		DOTweenLocalResizeTop		(this RectTransform rt, float to,	float duration)	=> DOTween.To(() => rt.LocalTopY(),		rt.SetLocalTop,			to, duration);

	public static Tween		DOTweenResize				(this RectTransform rt, Vector2 to, float duration)	=> DOTween.To(() => rt.sizeDelta,		x => rt.sizeDelta = x,	to, duration);
}

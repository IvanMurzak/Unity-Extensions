using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

#if UNITY_EDITOR
using DG.DOTweenEditor;
#endif

[Serializable]
public abstract class TransitionAnimation<T> : MonoBehaviour, ITransitionAnimation where T : Component
{
	[Required]
	public	T				target;
	public	float			duration		= 1;
	public	Ease			ease			= Ease.InOutSine;
	public	Ease			easeBack		= Ease.InOutSine;

	new		Tween			animation;
			Tween			animationBack;

	protected abstract Tween CreateAnimation		{ get; }
	protected abstract Tween CreateAnimationBack	{ get; }

	[HorizontalGroup("Buttons"), Button(ButtonSizes.Medium)]
	public void Animate()
	{
		StopAllAnimations();
		this.animation = CreateAnimation;

#if UNITY_EDITOR
		if (!Application.isPlaying)
		{
			var animation = this.animation;
			DOTweenEditorPreview.PrepareTweenForPreview(animation);
			DOTweenEditorPreview.Start();
		}
#endif
	}
	[HorizontalGroup("Buttons"), Button(ButtonSizes.Medium)]
	public void AnimateBack()
	{
		StopAllAnimations();
		animationBack = CreateAnimationBack;

#if UNITY_EDITOR
		if (!Application.isPlaying)
		{
			var animation = this.animationBack;
			DOTweenEditorPreview.PrepareTweenForPreview(animationBack);
			DOTweenEditorPreview.Start();
		}
#endif
	}
	public void StopAllAnimations()
	{
#if UNITY_EDITOR
		if (!Application.isPlaying)
		{
			DOTweenEditorPreview.Stop();
		}
#endif
		if (animation?.active ?? false) animation?.Kill();
		if (animationBack?.active ?? false) animationBack?.Kill();
	}
}
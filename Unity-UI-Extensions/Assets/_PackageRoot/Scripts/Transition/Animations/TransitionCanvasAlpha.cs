using DG.Tweening;
using UnityEngine;

public class TransitionCanvasAlpha : TransitionAnimation<CanvasGroup>
{
	public	bool		relative;
	public	float		to;
	
			float		startAlpha;

			float		To => relative ? to + startAlpha : to;

	private void Awake()
	{
		startAlpha = target.alpha;
	}
	
	protected override Tween CreateAnimation		=> target.DOFade(To, duration).SetEase(ease);
	protected override Tween CreateAnimationBack	=> target.DOFade(startAlpha, duration).SetEase(easeBack);
}
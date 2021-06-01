using DG.Tweening;
using UnityEngine;

public class TransitionResizeTop : TransitionAnimation<RectTransform>
{
	public	bool		relative;
	public	float		to;
	
			float		start;
	
			float		To			=> relative ? start + to : to;

	private void Awake()
	{
		start = target.LocalTopY();
	}

	protected override Tween CreateAnimation		=> target.DOTweenLocalResizeTop(To, duration).SetEase(ease);
	protected override Tween CreateAnimationBack	=> target.DOTweenLocalResizeTop(start, duration).SetEase(easeBack);
}

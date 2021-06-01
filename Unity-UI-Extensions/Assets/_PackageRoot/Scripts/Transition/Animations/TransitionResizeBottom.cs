using DG.Tweening;
using UnityEngine;

public class TransitionResizeBottom : TransitionAnimation<RectTransform>
{
	public	bool		relative;
	public	float		to;
	
			float		start;
	
			float		To			=> relative ? start + to : to;

	private void Awake()
	{
		start = target.LocalBottomY();
	}

	protected override Tween CreateAnimation		=> target.DOTweenLocalResizeBottom(To, duration).SetEase(ease);
	protected override Tween CreateAnimationBack	=> target.DOTweenLocalResizeBottom(start, duration).SetEase(easeBack);
}

using DG.Tweening;
using UnityEngine;

public class TransitionAnchorMax : TransitionAnimation<RectTransform>
{
	public	bool		relative;
	public	Vector2		to;
	
			Vector2		startPosition;

			Vector2		To => relative ? to + startPosition : to;

	private void Awake()
	{
		startPosition = target.anchorMax;
	}
	
	protected override Tween CreateAnimation		=> target.DOAnchorMax(To, duration).SetEase(ease);
	protected override Tween CreateAnimationBack	=> target.DOAnchorMax(startPosition, duration).SetEase(easeBack);
}
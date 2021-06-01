using DG.Tweening;
using UnityEngine;

public class TransitionAnchorMove : TransitionAnimation<RectTransform>
{
	public	bool		relative;
	public	Vector2		to;
	
			Vector2		startPosition;

			Vector2		To => relative ? to + startPosition : to;

	private void Awake()
	{
		startPosition = target.anchoredPosition;
	}
	
	protected override Tween CreateAnimation		=> target.DOAnchorPos(To, duration).SetEase(ease);
	protected override Tween CreateAnimationBack	=> target.DOAnchorPos(startPosition, duration).SetEase(easeBack);
}
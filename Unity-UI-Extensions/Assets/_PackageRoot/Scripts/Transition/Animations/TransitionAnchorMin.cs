using DG.Tweening;
using UnityEngine;

public class TransitionAnchorMin : TransitionAnimation<RectTransform>
{
	public	bool		relative;
	public	Vector2		to;
	
			Vector2		startPosition;

			Vector2		To => relative ? to + startPosition : to;

	private void Awake()
	{
		startPosition = target.anchorMin;
	}
	
	protected override Tween CreateAnimation		=> target.DOAnchorMin(To, duration).SetEase(ease);
	protected override Tween CreateAnimationBack	=> target.DOAnchorMin(startPosition, duration).SetEase(easeBack);
}
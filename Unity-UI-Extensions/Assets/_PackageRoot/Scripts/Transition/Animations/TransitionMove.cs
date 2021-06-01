using DG.Tweening;
using UnityEngine;

public class TransitionMove : TransitionAnimation<RectTransform>
{
	public	bool		relative;
	public	Vector3		to;
	
			Vector3		startPosition;

			Vector3		To => relative ? to + startPosition : to;

	private void Awake()
	{
		startPosition = target.localPosition;
	}
	
	protected override Tween CreateAnimation		=> target.DOLocalMove(To, duration).SetEase(ease);
	protected override Tween CreateAnimationBack	=> target.DOLocalMove(startPosition, duration).SetEase(easeBack);
}
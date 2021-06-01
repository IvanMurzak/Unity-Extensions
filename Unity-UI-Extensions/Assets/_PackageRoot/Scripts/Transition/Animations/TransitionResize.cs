using DG.Tweening;
using UnityEngine;

public class TransitionResize : TransitionAnimation<RectTransform>
{
	public	bool		relative;
	public	Vector2		to;
	
			Vector2		start;
	
			Vector2		To			=> relative ? start + to : to;

	private void Awake()
	{
		start = target.sizeDelta;
	}

	protected override Tween CreateAnimation		=> target.DOTweenResize(To, duration).SetEase(ease);
	protected override Tween CreateAnimationBack	=> target.DOTweenResize(start, duration).SetEase(easeBack);
}

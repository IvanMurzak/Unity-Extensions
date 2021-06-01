using DG.Tweening;
using UnityEngine;

public class TransitionRotate : TransitionAnimation<RectTransform>
{
	public bool			relative;
	public Vector3		to;

	protected override Tween CreateAnimation		=> target.DOLocalRotate(to, duration).SetEase(ease);
	protected override Tween CreateAnimationBack	=> target.DOLocalRotate(to, duration).SetEase(easeBack);
}
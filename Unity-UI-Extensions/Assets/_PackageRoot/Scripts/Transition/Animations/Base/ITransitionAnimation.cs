using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

public interface ITransitionAnimation
{
	void Animate();
	void AnimateBack();
	void StopAllAnimations();
}
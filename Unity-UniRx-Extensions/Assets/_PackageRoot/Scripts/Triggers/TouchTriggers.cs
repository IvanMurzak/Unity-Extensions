using UniRx;
using UniRx.Triggers;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class TouchTriggers : ObservableTriggerBase
{
	Subject<Touch> onTouchDown;

	public IObservable<Touch> OnTouchDownGlobalAsObservable()
	{
		if (onTouchDown == null) onTouchDown = new Subject<Touch>();
		return onTouchDown;
	}


	Subject<Touch> onTouchUp = new Subject<Touch>();

	public IObservable<Touch> OnTouchUpGlobalAsObservable()
	{
		if (onTouchUp == null) onTouchUp = new Subject<Touch>();
		return onTouchUp;
	}


	private void Update()
	{
		if (onTouchDown != null && Input.touchCount > 0)
		{
			if (Input.GetTouch(0).phase == TouchPhase.Began)
				onTouchDown.OnNext(Input.GetTouch(0));
		}

		if (onTouchUp != null && Input.touchCount > 0)
		{
			if (Input.GetTouch(0).phase == TouchPhase.Ended)
				onTouchUp.OnNext(Input.GetTouch(0));
		}
	}


	protected override void RaiseOnCompletedOnDestroy()
	{
		if (onTouchDown != null)
		{
			onTouchDown.OnCompleted();
			onTouchDown = null;
		}
		if (onTouchUp != null)
		{
			onTouchUp.OnCompleted();
			onTouchUp = null;
		}
	}
}
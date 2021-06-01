using UniRx;
using System;
using UnityEngine;
using Sirenix.OdinInspector;

public abstract class ReactiveButton : MonoBehaviour
{
    [NonSerialized] public readonly IObservable<Unit> onClick			= new Subject<Unit>();
    [NonSerialized] public readonly IObservable<Unit> onClickDisabled	= new Subject<Unit>();

	public virtual void Invoke()
    {
		if (gameObject.activeInHierarchy && enabled)
		{
			OnBeforeClick();
			((Subject<Unit>)onClick).OnNext(Unit.Default);
			OnAfterClick();
		}
		else
		{
			OnBeforeClickDisabled();
			((Subject<Unit>)onClickDisabled).OnNext(Unit.Default);
			OnAfterClickDisabled();
		}
    }

	protected virtual void OnBeforeClick			() { }
	protected virtual void OnBeforeClickDisabled	() { }
	protected virtual void OnAfterClick				() { }
	protected virtual void OnAfterClickDisabled		() { }
}

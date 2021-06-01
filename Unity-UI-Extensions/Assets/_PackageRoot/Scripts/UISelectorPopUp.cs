using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine.EventSystems;

public abstract class UISelectorPopUp<T> : UIPopUp where T : Component
{
    [NonSerialized] public IObservable<T> OnOpen = new Subject<T>();
    [NonSerialized] public IObservable<T> OnClose = new Subject<T>();

	protected T lastUnit { get; private set; }

	protected override void Awake()
	{
		base.Awake();

		var onOpen = OnOpenObservable();
		if (onOpen != null) onOpen.Subscribe(x => Open(x)).AddTo(this);

		var onClose = OnCloseObservable();
		if (onClose != null) onClose.Subscribe(Close).AddTo(this);

		IsOpenObservable.Where(x => x)
			.Subscribe(x =>
			{
				Observable.Merge
					(
						EventSystem.current.OnMouseDownGlobalAsObservable(0),
						EventSystem.current.OnTouchDownGlobalAsObservable().Select(touch => touch.position)
					)
					.TakeUntil(OnClose)
					.Subscribe(OnPress)
					.AddTo(this);
			})
			.AddTo(this);
    }

    private void OnPress(Vector2 position)
    {
		var selected = EventSystem.current.currentSelectedGameObject;
		if (IsOpen && (selected == null || IsOther(selected.transform)))
        {
            Close(lastUnit);
        }
    }

    protected bool IsOther(Transform selected)
    {
        if (selected == null) return true;
        if (selected == transform) return false;
        return IsOther(selected.parent);
    }

    protected virtual void Close(T unit)
    {
        if (!IsOpen)
        {
            Close();
            return;
        }
        
        Close();
		DebugFormat.Log(this, gameObject.name);
        ((Subject<T>)OnClose).OnNext(lastUnit);
        lastUnit = null;
    }

    protected virtual bool Open(T unit)
    {
        if (IsOpen) return Open();
		DebugFormat.Log(this, gameObject.name);
		((Subject<T>)OnOpen).OnNext(lastUnit = unit);
        return Open();
    }

    protected abstract IObservable<T> OnOpenObservable();
    protected abstract IObservable<T> OnCloseObservable();
}

using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class MouseTriggers : ObservableTriggerBase
{
    Dictionary<int, Subject<Vector2>> onMouseDown = new Dictionary<int, Subject<Vector2>>();

    public IObservable<Vector2> OnMouseDownGlobalAsObservable(int button)
    {
        if (onMouseDown.ContainsKey(button)) return onMouseDown[button];
        var observable = new Subject<Vector2>();
        onMouseDown.Add(button, observable);
        return observable;
    }


    Subject<Vector3> onMouseDrag;
    Vector3 previousMousePosition;

    public IObservable<Vector3> OnMouseDragGlobalAsObservable()
    {
        return onMouseDrag ?? (onMouseDrag = new Subject<Vector3>());
    }


    Dictionary<int, Subject<Vector2>> onMouseUp = new Dictionary<int, Subject<Vector2>>();

    public IObservable<Vector2> OnMouseUpGlobalAsObservable(int button)
    {
        if (onMouseUp.ContainsKey(button)) return onMouseUp[button];
        var observable = new Subject<Vector2>();
        onMouseUp.Add(button, observable);
        return observable;
    }



    private void Awake()
    {
        previousMousePosition = Input.mousePosition;
    }

    private void Update()
    {
        if (onMouseDown.Count > 0)
        {
            foreach(var button in onMouseDown.Keys)
            {
                if (Input.GetMouseButtonDown(button))
                    onMouseDown[button].OnNext(Input.mousePosition);
            }
        }


        if (onMouseDrag != null)
        {
            if (previousMousePosition != Input.mousePosition)
            {
                var delta = previousMousePosition - Input.mousePosition;
                previousMousePosition = Input.mousePosition;
                onMouseDrag.OnNext(delta);
            }
        }

        if (onMouseUp.Count > 0)
        {
            foreach (var button in onMouseUp.Keys)
            {
                if (Input.GetMouseButtonUp(button))
                    onMouseUp[button].OnNext(Input.mousePosition);
            }
        }
    }


    protected override void RaiseOnCompletedOnDestroy()
    {
        foreach (var observable in onMouseDown.Values)
            observable.OnCompleted();

        if (onMouseDrag != null)
            onMouseDrag.OnCompleted();

        foreach (var observable in onMouseUp.Values)
            observable.OnCompleted();
    }
}

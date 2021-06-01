using UnityEngine;
using UniRx;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIReactiveButton : ReactiveButton 
{
    protected virtual void Awake()
    {
		var button = GetComponent<Button>();
        button.onClick.AsObservable().Subscribe(x => Invoke());
    }
}

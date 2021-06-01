using UnityEngine;
using UniRx;

[RequireComponent(typeof(ReactiveButton))]
public class ReactiveButtonLogger : MonoBehaviour
{
	private void Awake()
	{
		var button = GetComponent<ReactiveButton>();
			button.onClick			.Subscribe(x => Debug.Log("onClick", this))			.AddTo(this);
			button.onClickDisabled	.Subscribe(x => Debug.Log("onClickDisabled", this))	.AddTo(this);
	}
}

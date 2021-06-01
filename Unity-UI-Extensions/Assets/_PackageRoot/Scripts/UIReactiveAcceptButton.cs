using System;
using UniRx;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.UI;
using Sirenix.Utilities;

public class UIReactiveAcceptButton : UIReactiveButton
{
	[NonSerialized] Subject<Unit>	onAccepted = new Subject<Unit>();
	public IObservable<Unit>		OnAccepted => onAccepted;
	[NonSerialized] Subject<Unit>	onClickNormal = new Subject<Unit>();
	public IObservable<Unit>		OnClickNormal => onClickNormal;

	[NonSerialized, ReadOnly, ShowInInspector] public readonly BoolReactiveProperty isAcceptMode = new BoolReactiveProperty(false);

	[FoldoutGroup("Settings"), HorizontalGroup("Settings/1")] public Graphic[] onNormal;
	[FoldoutGroup("Settings"), HorizontalGroup("Settings/1")] public Graphic[] onAccepting;

    [FoldoutGroup("Settings")] public bool	autoSwitchStates	= true;
    [FoldoutGroup("Settings")] public float animationSpeed		= 5f;
    [FoldoutGroup("Settings")] public Ease	animationEase		= Ease.OutQuad;

	private bool acceptStateBeforeClick;

	Graphic[] ToShow(bool isAccept) => isAccept ? onAccepting : onNormal;
	Graphic[] ToHide(bool isAccept) => isAccept ? onNormal : onAccepting;

	protected override void Awake()
	{
		base.Awake();

		ForceState();
		isAcceptMode.SkipLatestValueOnSubscribe().Subscribe(OnModeChanged).AddTo(this);
		onClick.Where(x => isAcceptMode.Value).Subscribe(onAccepted.OnNext).AddTo(this);
		onClick.Where(x => !isAcceptMode.Value).Subscribe(onClickNormal.OnNext).AddTo(this);
	}

	void ForceState		()
	{
		ToShow(isAcceptMode.Value).ForEach(x => Set(x, 1));
		ToHide(isAcceptMode.Value).ForEach(x => Set(x, 0));
	}
	void OnModeChanged	(bool isAccept)
	{
		ToShow(isAccept).ForEach(x => Animate(x, 1));
		ToHide(isAccept).ForEach(x => Animate(x, 0));
	}
	void Set			(Graphic image, float alpha)
	{
		image.color = image.color.SetA(alpha);
		image.enabled = alpha != 0;
	}
	void Animate		(Graphic image, float alpha)
	{
		DOTween.Kill(image.GetInstanceID());
		DOTween.ToAlpha(() => image.color, c => image.color = c, alpha, animationSpeed)
			.SetSpeedBased	()
			.SetEase		(animationEase)
			.SetId			(image.GetInstanceID())
			.OnStart		(() => image.enabled = true)
			.OnComplete		(() => image.enabled = alpha != 0);
	}

	protected override void OnBeforeClick()
	{
		base.OnBeforeClick();
		acceptStateBeforeClick = isAcceptMode.Value;
	}
	protected override void OnAfterClick()
	{
		base.OnAfterClick();

		if (autoSwitchStates && isAcceptMode.Value == acceptStateBeforeClick)
		{
			DebugFormat.Log(this, "Auto switch");
			isAcceptMode.Value = !isAcceptMode.Value;
		}
	}
}

using System;
using System.Linq;
using DG.Tweening;
using UniRx;

public class UIPopUp : BaseMonoBehaviour
{
	[NonSerialized] public static readonly IObservable<UIPopUp> OnOpenAny	= new Subject<UIPopUp>();
	[NonSerialized] public static readonly IObservable<UIPopUp> OnCloseAny	= new Subject<UIPopUp>();

    protected	DOTweenAnimation[]		animations;

	private		BoolReactiveProperty	_isOpen				= new BoolReactiveProperty(false);

	public		bool					IsOpen				=> _isOpen.Value;
	public		IObservable<bool>		IsOpenObservable	=> _isOpen.SkipLatestValueOnSubscribe();

	private		IObserver<UIPopUp>		OnOpenAnyObserver	=> OnOpenAny as IObserver<UIPopUp>;
	private		IObserver<UIPopUp>		OnCloseAnyObserver	=> OnCloseAny as IObserver<UIPopUp>;

				CompositeDisposable		openCloseDisposable = new CompositeDisposable();

	protected virtual void Awake()
	{
		_isOpen.Where(x => x).Subscribe(x => OnOpenAnyObserver.OnNext(this)).AddTo(this);
		_isOpen.Where(x => !x).Subscribe(x => OnCloseAnyObserver.OnNext(this)).AddTo(this);

		openCloseDisposable.AddTo(this);

		animations = GetComponents<DOTweenAnimation>();
		
		SubscribeOnAnimationComplete();
	}

	protected virtual void Start()
	{
		gameObject.SetActive(IsOpen);
	}

    protected virtual void Close(bool force = false)
    {
        if (!IsOpen) return;

		foreach (var animation in animations)
		{
			animation.DOPlayBackwards();
			if (force) animation.DOKill(true);
		}
		_isOpen.Value = false;
    }

    protected virtual bool Open(bool force = false)
    {
        if (IsOpen) return false;
		gameObject.SetActive(true);

		foreach (var animation in animations)
		{
			animation.DORestart();
			if (force) animation.DOKill(true);
		}
		_isOpen.Value = true;
		return true;
    }
	protected virtual void OnShowAnimationCompleted()
	{
		DebugFormat.Log(this);
	}
	protected virtual void OnHideAnimationCompleted()
	{
		DebugFormat.Log(this);
		gameObject.SetActive(false);
	}

	void SubscribeOnAnimationComplete()
	{
		var longestAnimation = animations.OrderBy(x => x.delay + x.duration).FirstOrDefault();
		if (longestAnimation != null)
		{
			longestAnimation.hasOnStepComplete = true;
			longestAnimation.onStepComplete.AddListener(() =>
			{
				if (IsOpen) OnShowAnimationCompleted();
				else OnHideAnimationCompleted();
			});
		}
	}
}
using System;
using UniRx;

public static class DelayExecution
{
    public static IDisposable Frame<Unit>		(Action<UniRx.Unit> action)						=> Frame(1, action);
    public static IDisposable Frame				(int frameCount, Action<UniRx.Unit> action)		=> Frame<UniRx.Unit>(frameCount, action, Unit.Default);
    public static IDisposable Frame<T>			(int frameCount, Action<T> action, T emit)		=> Observable.TimerFrame(frameCount).Subscribe(x => action.Invoke(emit));

    // ------------------------------------------------------------------------------------

    public static IDisposable Time<Unit>		(Action<UniRx.Unit> action)						=> Time(TimeSpan.FromSeconds(1), action);
    public static IDisposable Time				(float dueTime, Action<UniRx.Unit> action)		=> Time(TimeSpan.FromSeconds(dueTime), action);
    public static IDisposable Time				(TimeSpan dueTime, Action<UniRx.Unit> action)	=> Time<UniRx.Unit>(dueTime, action, Unit.Default);
    public static IDisposable Time<T>			(float dueTime, Action<T> action, T emit)		=> Time(TimeSpan.FromSeconds(dueTime), action, emit);
    public static IDisposable Time<T>			(TimeSpan dueTime, Action<T> action, T emit)	=> Observable.Timer(dueTime).Subscribe(x => action.Invoke(emit));

	// ------------------------------------------------------------------------------------

	public static IDisposable Interval			(float duration, float period, Action<UniRx.Unit> action, Action<UniRx.Unit> onComplete = null)			=> Interval(TimeSpan.FromSeconds(duration), TimeSpan.FromSeconds(period), action, onComplete);
	public static IDisposable Interval			(TimeSpan duration, TimeSpan period, Action<UniRx.Unit> action, Action<UniRx.Unit> onComplete = null)	=> Interval<UniRx.Unit>(duration, period, action, onComplete, Unit.Default);
	public static IDisposable Interval<T>		(float duration, float period, Action<T> action, Action<T> onComplete, T emit)							=> Interval(TimeSpan.FromSeconds(duration), TimeSpan.FromSeconds(period), action, onComplete, emit);
	public static IDisposable Interval<T>		(TimeSpan duration, TimeSpan period, Action<T> action, Action<T> onComplete, T emit)
	{
		var disposable	= new CompositeDisposable();
		var timeout		= Observable.Timer(duration);
		var timer		= Observable.Interval(period).TakeUntil(timeout);

		timeout.Subscribe(x =>
		{
			action?		.Invoke(emit);
			onComplete?	.Invoke(emit);
		}).AddTo(disposable);

		timer.Subscribe(x => action?.Invoke(emit))
			.AddTo(disposable);

		return disposable;
	}

	// ------------------------------------------------------------------------------------

	public static IDisposable Times				(float period, int times, Action<UniRx.Unit> action, Action<UniRx.Unit> onComplete)			=> Times(TimeSpan.FromSeconds(period), times, action, onComplete);
	public static IDisposable Times				(TimeSpan period, int times, Action<UniRx.Unit> action, Action<UniRx.Unit> onComplete)		=> Times<UniRx.Unit>(period, times, action, onComplete, Unit.Default);
	public static IDisposable Times<T>			(float period, int times, Action<T> action, Action<T> onComplete, T emit)					=> Times(TimeSpan.FromSeconds(period), times, action, onComplete, emit);
	public static IDisposable Times<T>			(TimeSpan period, int times, Action<T> action, Action<T> onComplete, T emit)
	{
		var timer = Observable.Timer(period).Repeat().TakeWhile(x => times-- > 0);
			timer.DoOnCompleted(() => onComplete.Invoke(emit));

		return timer.Subscribe(x => action.Invoke(emit));
	}
}
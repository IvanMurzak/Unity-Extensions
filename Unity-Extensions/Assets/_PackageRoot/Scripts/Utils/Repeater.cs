using System;
using UniRx;

public class Repeater
{
    private IDisposable disposable;
    private Action action;

    public bool IsActive { get { return disposable != null; } }

    public Repeater(Action action)
    {
        this.action = action;
    }

    public void Start(float interval)
    {
        Stop();
        var delay = TimeSpan.FromSeconds(interval);
        disposable = Observable.Timer(TimeSpan.FromSeconds(0), delay).Subscribe(x => action.Invoke());
    }

    public void Stop()
    {
        disposable?.Dispose();
        disposable = null;
    }
}

using System;
using UniRx;
using UnityEngine;
using System.IO;

[Serializable]
public class SuperTimer
{
						CompositeDisposable		compositeDisposable = new CompositeDisposable();
						string					savePath;
						bool					subscribed;
						Action					onTriggered;
						DateTime?				endTime;
	public				DateTime?				EndTime
												{
													get => endTime;
													set
													{
														endTime = value;
														Save(value);
														Start();
													}
												}

	protected virtual	DateTime				Now					=> DateTime.Now;
	public				bool					IsActive			=> endTime == null ? false : endTime.Value > Now;
	public				bool					IsCached			=> File.Exists(savePath);
	public virtual		DateTime?				Load				()
	{
		if (!IsCached)		return null;
		return				Saver<DateTime?>.Load(savePath);
	}
	public virtual		void					Save				(DateTime? time)
	{
		if (time == null)	Saver<DateTime?>.Delete(savePath);
		else				Saver<DateTime?>.Save(time, savePath);
	}

	public SuperTimer(string savePath, Action onTriggered)
	{
		this.savePath		= savePath;
		this.onTriggered	= onTriggered;
		this.EndTime		= Load();
	}
	public SuperTimer(string savePath, DateTime endTime, Action onTriggered)
	{
		this.savePath		= savePath;
		this.onTriggered	= onTriggered;
		this.EndTime		= endTime;
	}
	public SuperTimer(string savePath, TimeSpan duration, Action onTriggered)
	{
		this.savePath		= savePath;
		this.onTriggered	= onTriggered;
		this.EndTime		= Now + duration;
	}

	private void Start()
	{
		if (IsActive)
		{
			if (subscribed) return;

			subscribed = true;
			compositeDisposable.Clear();

			Observable.Timer(endTime.Value - Now)
				.Subscribe(_ =>
				{
					try { onTriggered?.Invoke(); }
					catch (Exception e) { Debug.LogException(e); }
					Cancel();
				})
				.AddTo(compositeDisposable);
		}
		else if (IsCached)
		{
			try { onTriggered?.Invoke(); }
			catch (Exception e) { Debug.LogException(e); }
			Cancel();
		}
	}
	public void Cancel()
	{
		compositeDisposable.Clear();
		EndTime		= null;
		subscribed	= false;
	}
	public SuperTimer AddTo(MonoBehaviour gameObjectComponent)
	{
		compositeDisposable.AddTo(gameObjectComponent);
		return this;
	}
}
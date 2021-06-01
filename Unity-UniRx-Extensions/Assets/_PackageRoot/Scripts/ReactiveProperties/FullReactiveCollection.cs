using System;
using System.Collections.ObjectModel;
using UniRx;

public class FullReactiveCollection<T> : Collection<ReactiveProperty<T>>
{
			Subject<(int, T)>		onValueChanged	= new Subject<(int, T)>();
	public	IObservable<(int, T)>	OnValueChanged	=> onValueChanged;

			Collection<IDisposable> disposables		= new Collection<IDisposable>();

	public FullReactiveCollection() : base() { }
	public FullReactiveCollection(Collection<ReactiveProperty<T>> collection) : base(collection) { }

	protected override void ClearItems()
	{
		base.ClearItems();

		foreach (var disposable in disposables)
		{
			disposable.Dispose();
		}
		disposables.Clear();
	}
	protected override void InsertItem(int index, ReactiveProperty<T> item)
	{
		base.InsertItem(index, item);

		disposables.Insert(index, item.Subscribe(x => onValueChanged.OnNext((index, x))));
	}
	protected override void RemoveItem(int index)
	{
		base.RemoveItem(index);

		disposables[index].Dispose();
		disposables.RemoveAt(index);
	}
	protected override void SetItem(int index, ReactiveProperty<T> item)
	{
		this[index].Value = item.Value;
		// base.SetItem(index, item);
	}
}
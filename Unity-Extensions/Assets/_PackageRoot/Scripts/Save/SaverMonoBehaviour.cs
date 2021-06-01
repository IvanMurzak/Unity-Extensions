using System;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using Sirenix.Serialization;
using UniRx;

public abstract class SaverMonoBehaviour<T> : BaseMonoBehaviour, IPreBuildSetup
{
	private				Subject<T>					onSaveStarted			= new Subject<T>();
	private				Subject<T>					onDataLoaded			= new Subject<T>();
	private				Subject<T>					onDataModified			= new Subject<T>();

	[NonSerialized, OdinSerialize, Required, DisableInPrefabAssets]
    [BoxGroup("Saving", false), BoxGroup("Saving/Data", false)/*, ShowIf("HasSaver")*/]
    private				Saver<T>					saver;

	private				ISaverOnLoadedListener<T>[]	loadingDataListeners	= new ISaverOnLoadedListener<T>[0];

	public				bool						Loaded					=> saver.Loaded;

	public				IObservable<T>				OnSaveStarted			=> onSaveStarted;
	public				IObservable<T>				OnDataLoaded			=> onDataLoaded;
	public				IObservable<T>				OnDataModified			=> onDataModified;

	protected			T							Data					{ get; set; }
	protected			T							DefaultData				=> saver.DefaultData;
	
	protected virtual	string						SaverPath				=> "Savers";
	protected virtual	string						SaverFileName			=> this.FullName('_');

	private				bool						HasSaver				=> saver != null;


	public virtual void PreBuildSetup()
	{
		InitSaver();
	}
	[HorizontalGroup("Managering Data"), Button(ButtonSizes.Medium)]
	public T Load()
	{
		if (saver == null) DebugFormat.LogError(this, "Saver is not initialized!");
		Data = PrepareData(saver.Load());
		NotifyLoadingDataListeners(loadingDataListeners, Data);
		return Data;
	}
	public Task LoadAsync()
	{
		if (saver == null) DebugFormat.LogError(this, "Saver is not initialized!");
		return saver.LoadAsync(task =>
		{
			Data = PrepareData(saver.data);
			NotifyLoadingDataListeners(loadingDataListeners, Data);
		});
	}
	[HorizontalGroup("Managering Data"), Button(ButtonSizes.Medium)]
	public void Save()
	{
		if (saver == null) DebugFormat.LogError(this, "Saver is not initialized!");
		saver.data = PrepeareDataBeforeSave(Data);
		saver.Save();
		onSaveStarted.OnNext(Data);
	}
	public void SaveAsync()
	{
		if (saver == null) DebugFormat.LogError(this, "Saver is not initialized!");
		saver.data = PrepeareDataBeforeSave(Data);
		saver.SaveAsync();
		onSaveStarted.OnNext(Data);
	}
	public void SaveAsyncDelayed()
	{
		if (saver == null) DebugFormat.LogError(this, "Saver is not initialized!");
		saver.data = PrepeareDataBeforeSave(Data);
		saver.SaveAsyncDelayed();
		onSaveStarted.OnNext(Data);
	}


	protected virtual void Start()
    {
		PreBuildSetup();

		loadingDataListeners = GetComponents<ISaverOnLoadedListener<T>>();
        var dataModifiers = GetComponents<ISaverDataModifier<T>>();

		ObserveDataModifiers(dataModifiers);
		Load();
    }
    protected virtual T PrepareData(T data)
    {
        return data;
    }
	protected virtual T PrepeareDataBeforeSave(T data) { return data; }
    protected virtual void NotifyLoadingDataListeners(ISaverOnLoadedListener<T>[] dataListeners, T data)
    {
		dataListeners.ForEach(x => x.OnLoaded(data));
		onDataLoaded.OnNext(data);
	}
    protected virtual void ObserveDataModifiers(ISaverDataModifier<T>[] dataModifiers)
    {
        dataModifiers.ForEach(x => x.RegisterDataModifiedListener(OnDataModifiedByModifier));
    }
    protected virtual void OnDataModifiedByModifier(T data)
    {
		Data = data;
        saver.data = Data;
		onDataModified.OnNext(data);
		saver.SaveAsyncDelayed();
    }


	[BoxGroup("Saving", false), Button(ButtonSizes.Medium), GUIColor(1, .6f, .4f, 1), ShowIf("HasSaver")]
	private void UpdatePath()
	{
		saver.UpdatePath(SaverPath, SaverFileName);
	}
	[BoxGroup("Saving", false), Button(ButtonSizes.Large), GUIColor(.6f, 1, .4f, 1), HideIf("HasSaver")]
	private void InitSaver()
	{
		if (saver == null)	saver = new Saver<T>(SaverPath, SaverFileName);
		else				saver.UpdatePath(SaverPath, SaverFileName);
		// PrefabUtils.EditorApplyPrefabChanges(this);
	}
	private void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)	Save();
	}
#if UNITY_EDITOR
	private void OnDisable()
	{
		Save();
	}
#endif
}

public interface ISaverOnLoadedListener<T>
{
    void OnLoaded(T data);
}
public interface ISaverDataModifier<T>
{
    void RegisterDataModifiedListener(Action<T> onModified);
}

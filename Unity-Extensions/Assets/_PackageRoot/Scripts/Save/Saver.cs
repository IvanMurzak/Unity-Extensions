using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System;
using Sirenix.Serialization;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using UniRx;

public class Saver<T> : ISavable, ILoadable<T>
{
	#region static
	private static readonly string		PERSISTANT_DATA_PATH		= Application.persistentDataPath + "/Save";

	public	static readonly	TaskFactory factory						= new TaskFactory(new LimitedConcurrencyLevelTaskScheduler(1));

	public	static			string		FullPath					(string path)					=> $"{PERSISTANT_DATA_PATH}/{path}";
	public	static			string		FullPath					(string path, string fileName)	=> $"{PERSISTANT_DATA_PATH}/{path}/{fileName}";

	private static			bool		IsFileExists				(string path, string fileName)
	{
		var fullPath = FullPath(path, fileName);
		return File.Exists(fullPath);
	}

	public static			void		Save						(T data, string path, string fileName)
	{
		Save(data, FullPath(path, fileName));
	}
	public static			void		Save						(T data, string fullPath)
	{
		Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
		var bytes = SerializationUtility.SerializeValue(data, DataFormat.Binary);
		File.WriteAllBytes(fullPath, EncryptionUtils.Encrypt(bytes));
	}
	public static			T			Load						(string path, string fileName)
	{
		return Load(FullPath(path, fileName));
	}
	public	static			T			Load						(string fullPath)
	{
		// Debug.Log($"Full Path: {fullPath}");
		// DebugFormat.Log<Saver<T>>($"Full Path: {fullPath}");
		T data;
		if (!File.Exists(fullPath))
		{
			data = Activator.CreateInstance<T>();
			return data;
		}
		byte[] bytes = File.ReadAllBytes(fullPath);
		try
		{
			data = SerializationUtility.DeserializeValue<T>(EncryptionUtils.Decrypt(bytes), DataFormat.Binary);
		}
		catch(Exception e)
		{
			// DebugFormat.LogException<Saver<T>>(e);
			data = default(T);
		}
		// DebugFormat.PrintObjectStructure(data);
		return data;
	}
	public	static			void		Delete						(string fullPath)
	{
		if (File.Exists(fullPath))
		{
			File.Delete(fullPath);
		}
	}
	public	static			void		DeleteAllSaves				()
	{
		DebugFormat.Log<Saver<T>>($"path: {PERSISTANT_DATA_PATH}");
		if (Directory.Exists(PERSISTANT_DATA_PATH))
			Directory.Delete(PERSISTANT_DATA_PATH, true);
	}
	private	static			void		CheckSerializableAttribute	()
	{
		var serializable = typeof(T).IsDefined(typeof(System.SerializableAttribute), false);
		if (!serializable) throw new IOException($"{typeof(T).Name} isn't Serializable");
	}
#endregion

	[NonSerialized] Subject<Unit> onSaveAsyncDelayed;

	[SerializeField, ShowInInspector, ReadOnly] private string path;
	[SerializeField, ShowInInspector, ReadOnly] private string fileName;

	[ShowInInspector, ReadOnly] float asyncDelayedSave = 1f;

	public bool Loaded { get; private set; } = false;

	[BoxGroup("Data", false), NonSerialized, OdinSerialize, ReadOnly] public T data;
	[BoxGroup("Data", false), NonSerialized, OdinSerialize] private T _defaultData;
	public T	DefaultData
	{
		private set => _defaultData = value;
		get
		{
			if (_defaultData != null && _defaultData is ICloneable)
			{
				return (T)((ICloneable)_defaultData).Clone();
			}
			else
			{
				return _defaultData;
			}
		}
	}

	[ShowInInspector, ReadOnly] private string fullPath => FullPath(path, fileName);

	[BoxGroup("Data", false), ButtonGroup("Data/Buttons2"), Button(ButtonSizes.Medium)]
	public void CopyFromDefault	()
	{
		data = DefaultData;
	}
    [BoxGroup("Data", false), ButtonGroup("Data/Buttons2"), Button(ButtonSizes.Medium), GUIColor(1, .6f, .4f, 1)]
    public void ClearDefaulData	()
    {
        _defaultData = default(T);
    }
    [BoxGroup("Data", false), ButtonGroup("Data/Buttons1"), Button(ButtonSizes.Medium)]
    public void ClearData		()
    {
        data = default(T);
    }
#if UNITY_EDITOR
	[BoxGroup("Data", false), ButtonGroup("Data/Buttons1"), Button(ButtonSizes.Medium), GUIColor(1, .3f, .3f, 1)]
	private void DeleteSave		()
	{
		var fullPath = FullPath(path, fileName);
		UnityEditor.FileUtil.DeleteFileOrDirectory(fullPath);
	}
#endif
	public		Saver()
	{
		DefaultData = Activator.CreateInstance<T>();
	}
    public		Saver			(string path, string fileName, float asyncDelayedSave = 1f) : this()
    {
        this.asyncDelayedSave = asyncDelayedSave;
        UpdatePath(path, fileName);
	}
    public void UpdatePath		(string path, string fileName)
    {
        this.path = path;
        this.fileName = fileName;
    }
    public bool IsFileExists	()
    {
        return IsFileExists(path, fileName);
    }
    public void Save			()
    {
        Save(data, path, fileName);
    }
    public Task SaveAsync		(Action<Task> onComplete = null)
    {
		// DebugFormat.Log<Saver<T>>();
        Task task = factory.StartNew(Save);
        if (onComplete != null) task = task.ContinueWith(onComplete);
        return task;
    }
    public void SaveAsyncDelayed()
    {
        if (onSaveAsyncDelayed == null)
        {
            onSaveAsyncDelayed = new Subject<Unit>();
            onSaveAsyncDelayed.Throttle(TimeSpan.FromSeconds(asyncDelayedSave))
                 .Subscribe(x => SaveAsync());
            // TODO: dispose it somewhere
        }
        if (Application.isPlaying) onSaveAsyncDelayed.OnNext(Unit.Default);
    }
    public T	Load			()
	{
		if (DefaultData == null)
		{
			DebugFormat.LogError(this, $"Default data is null. path={FullPath(path, fileName)}");
		}
		if (IsFileExists())
		{
			data = Load(path, fileName);
			if (data == null)
			{
				DebugFormat.LogError(this, $"Loaded data is null. path={FullPath(path, fileName)}");
				data = DefaultData;
			}			
		}
		else
		{
			data = DefaultData;
		}
		Loaded = true;
		return data;
    }
    public Task LoadAsync(Action<Task> onComplete = null)
    {
        Task task = factory.StartNew(Load);
        if (onComplete != null) task = task.ContinueWith(onComplete);
        return task;
    }


}

public interface ISavable
{
    [Button, HorizontalGroup("Save Buttons")]
    void Save();
}
public interface ILoadable<T>
{
    [Button, HorizontalGroup("Save Buttons")]
    T Load();
}

public class LimitedConcurrencyLevelTaskScheduler : TaskScheduler
{
    /// <summary>Whether the current thread is processing work items.</summary>
    [ThreadStatic]
    private static bool _currentThreadIsProcessingItems;
    /// <summary>The list of tasks to be executed.</summary>
    private readonly LinkedList<Task> _tasks = new LinkedList<Task>(); // protected by lock(_tasks)
                                                                       /// <summary>The maximum concurrency level allowed by this scheduler.</summary>
    private readonly int _maxDegreeOfParallelism;
    /// <summary>Whether the scheduler is currently processing work items.</summary>
    private int _delegatesQueuedOrRunning = 0; // protected by lock(_tasks)

    /// <summary>
    /// Initializes an instance of the LimitedConcurrencyLevelTaskScheduler class with the
    /// specified degree of parallelism.
    /// </summary>
    /// <param name="maxDegreeOfParallelism">The maximum degree of parallelism provided by this scheduler.</param>
    public LimitedConcurrencyLevelTaskScheduler(int maxDegreeOfParallelism)
    {
        if (maxDegreeOfParallelism < 1) throw new ArgumentOutOfRangeException("maxDegreeOfParallelism");
        _maxDegreeOfParallelism = maxDegreeOfParallelism;
    }

    /// <summary>Queues a task to the scheduler.</summary>
    /// <param name="task">The task to be queued.</param>
    protected sealed override void QueueTask(Task task)
    {
        // Add the task to the list of tasks to be processed.  If there aren't enough
        // delegates currently queued or running to process tasks, schedule another.
        lock (_tasks)
        {
            _tasks.AddLast(task);
            if (_delegatesQueuedOrRunning < _maxDegreeOfParallelism)
            {
                ++_delegatesQueuedOrRunning;
                NotifyThreadPoolOfPendingWork();
            }
        }
    }

    /// <summary>
    /// Informs the ThreadPool that there's work to be executed for this scheduler.
    /// </summary>
    private void NotifyThreadPoolOfPendingWork()
    {
        ThreadPool.UnsafeQueueUserWorkItem(_ =>
        {
            // Note that the current thread is now processing work items.
            // This is necessary to enable inlining of tasks into this thread.
            _currentThreadIsProcessingItems = true;
			try
			{
				// Process all available items in the queue.
				while (true)
				{
					Task item;
					lock (_tasks)
					{
						// When there are no more items to be processed,
						// note that we're done processing, and get out.
						if (_tasks.Count == 0)
						{
							--_delegatesQueuedOrRunning;
							break;
						}

						// Get the next item from the queue
						item = _tasks.First.Value;
						_tasks.RemoveFirst();
					}
					
					// Execute the task we pulled out of the queue
					base.TryExecuteTask(item);
				}
			}
			catch (Exception e) { Debug.LogException(e); }
			// We're done processing items on the current thread
			finally { _currentThreadIsProcessingItems = false; }
        }, null);
    }

    /// <summary>Attempts to execute the specified task on the current thread.</summary>
    /// <param name="task">The task to be executed.</param>
    /// <param name="taskWasPreviouslyQueued"></param>
    /// <returns>Whether the task could be executed on the current thread.</returns>
    protected sealed override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
    {
        // If this thread isn't already processing a task, we don't support inlining
        if (!_currentThreadIsProcessingItems) return false;

        // If the task was previously queued, remove it from the queue
        if (taskWasPreviouslyQueued) TryDequeue(task);

        // Try to run the task.
        return base.TryExecuteTask(task);
    }

    /// <summary>Attempts to remove a previously scheduled task from the scheduler.</summary>
    /// <param name="task">The task to be removed.</param>
    /// <returns>Whether the task could be found and removed.</returns>
    protected sealed override bool TryDequeue(Task task)
    {
        lock (_tasks) return _tasks.Remove(task);
    }

    /// <summary>Gets the maximum concurrency level supported by this scheduler.</summary>
    public sealed override int MaximumConcurrencyLevel { get { return _maxDegreeOfParallelism; } }

    /// <summary>Gets an enumerable of the tasks currently scheduled on this scheduler.</summary>
    /// <returns>An enumerable of the tasks currently scheduled.</returns>
    protected sealed override IEnumerable<Task> GetScheduledTasks()
    {
        bool lockTaken = false;
		try
		{
			Monitor.TryEnter(_tasks, ref lockTaken);
			if (lockTaken) return _tasks.ToArray();
			else throw new NotSupportedException();
		}
		catch (Exception e) { Debug.LogException(e); return null; }
		finally { if (lockTaken) Monitor.Exit(_tasks); }
    }
}

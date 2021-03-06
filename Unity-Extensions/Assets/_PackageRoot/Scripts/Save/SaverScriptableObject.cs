﻿using UnityEngine;
using Sirenix.OdinInspector;
using System.Threading.Tasks;

public abstract class SaverScriptableObject<T> : BaseScriptableObject, ISavable, ILoadable<T>
{
	[FoldoutGroup("Saving"), BoxGroup("Saving/Data", false), SerializeField, ShowInInspector, ShowIf("HasSaver")]
	private					Saver<T>	saver					= null;

	protected				T			Data					{ get; set; }
	protected				T			DefaultData				=> saver.DefaultData;

	protected	abstract	string		SaverPath				{ get; }
	protected	abstract	string		SaverFileName			{ get; }

	private					bool		HasSaver				=> saver != null;


	[HorizontalGroup("Managering Data"), Button(ButtonSizes.Medium)]
	public					T			Load()
	{
		Data = PrepareData(saver.Load());
		OnDataLoaded(Data);
		return Data;
	}
	public					Task		LoadAsync()				=> saver.LoadAsync(task => OnDataLoaded(Data = PrepareData(saver.data)));
	[HorizontalGroup("Managering Data"), Button(ButtonSizes.Medium)]
	public		virtual		void		Save()
	{
		saver.data = OnDataSave(Data);
		saver.Save();
	}
	public		virtual		void		SaveAsync()
	{
		saver.data = OnDataSave(Data);
		saver.SaveAsync();
	}
	public		virtual		void		SaveAsyncDelayed()
	{
		saver.data = OnDataSave(Data);
		saver.SaveAsyncDelayed();
	}


	protected	virtual		void		OnEnable()				=> Data = Load();
	protected	virtual		T			PrepareData(T data)		=> data;
	protected	abstract	void		OnDataLoaded(T data);
	protected	virtual		T			OnDataSave(T data)		=> data;


	[FoldoutGroup("Saving"), Button(ButtonSizes.Medium), GUIColor(1, .6f, .4f, 1), ShowIf("HasSaver")]
	private					void		UpdatePath()			=> saver.UpdatePath(SaverPath, SaverFileName);
	[FoldoutGroup("Saving"), Button(ButtonSizes.Large), GUIColor(.6f, 1, .4f, 1), HideIf("HasSaver")]
	private					void		InitSaver()
	{
		if (saver == null)
		{
			saver = new Saver<T>(SaverPath, SaverFileName);
		}
		else
		{
			saver.UpdatePath(SaverPath, SaverFileName);
		}
	}
}
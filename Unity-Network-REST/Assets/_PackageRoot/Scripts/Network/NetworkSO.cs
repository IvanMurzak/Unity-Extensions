using Cysharp.Threading.Tasks;
using Extensions.Saver;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;

namespace Network.Extension
{ 
	public abstract class NetworkSO : SaverScriptableObject<NetworkSO.SaveData>
	{
												public		const		long						AccessTokenExpiredCode		= 401;
															static		Color						AlarmColor()				=> "#FF7777".Parse(Color.red);

												protected	override	string						SaverPath					=> "network";
												protected	override	string						SaverFileName				=> $"network_{name}.db";

		// -------------------------------------------------------------------------------------------------------------		---------------------------------------

		[BoxGroup("Settings"), Required]		public					string						rootEndpoint;
		[BoxGroup("Settings")]					public					bool						debug						= true;
		[BoxGroup("Settings"), ShowIf("debug")]	public					bool						debugHeaders				= true;
		[BoxGroup("Settings")]
		[HideReferenceObjectPicker]				public					Dictionary<string, string>	rootHeaders					= new Dictionary<string, string>();

												public					RequestEvents<object>		GlobalEvents				{ get; } = new RequestEvents<object>();
												public					bool						IsAuthorized				=> !string.IsNullOrEmpty(Data?.accessToken);
												public					SaveData					GetData						=> Data;

																		CompositeDisposable			compositeDisposable			= new CompositeDisposable();

												protected	virtual		string						AccessTokenHeader
												{
													set
													{
														if (string.IsNullOrEmpty(value))
														{
															if (rootHeaders.ContainsKey("Cookie"))
																rootHeaders.Remove("Cookie");
														}
														else
														{
															rootHeaders["Cookie"] = $"{value}";
														}
													}
												}


		protected	override	void					OnEnable			()
		{
			AccessTokenHeader = null;

			base.OnEnable();
			DebugFormat.Log(this);

			compositeDisposable.Clear();
			GlobalEvents.OnHttpError.Where(httpError => httpError.httpResponseCode == AccessTokenExpiredCode)
				.Subscribe(httpError => SetToken(null, null))			
				.AddTo(compositeDisposable);
		}
		protected	override	void					OnDataLoaded		(SaveData data)
		{
			DebugFormat.Log(this);
			Data = new SaveData()
			{
				accessToken = data.accessToken,
				refreshToken = data.refreshToken
			};
			AccessTokenHeader = Data.accessToken;
		}


		public					void					SetToken			(string accessToken, string refreshToken)
		{
			Data.accessToken	= accessToken; // TODO: Data could be null should fix it!
			Data.refreshToken	= refreshToken;
			AccessTokenHeader	= Data.accessToken;
			SaveDelayed(TimeSpan.FromSeconds(1));
		}
		public async			UniTask<Request<T>>		SendRequest<T>		(Request<T> request)
		{
			var compositeDisposable = request.Events.Subscribe(GlobalEvents);

			if (debug)
			{
				var id = Request<T>.IncrementalID;

				DebugFormat.Log(this, $"[{request.RESTMethod}] {id} - {request.RequestURL}", this);
				if (debugHeaders)
				{
					var headers = request.GetHeaders();
					foreach (var key in headers.Keys)
					{
						DebugFormat.Log(this, $"Header {key} : {headers[key]}");
					}
				}
				var debugEvents = new DebugRequestEvents<T>(compositeDisposable, request, id);
				compositeDisposable.Add(request.Events.Subscribe(debugEvents));
			}

			await request.InternalSendRequest();
			if (debug && debugHeaders && request.LastUnityRequest != null)
			{
				if (debugHeaders)
				{
					var headers = request.LastUnityRequest.GetResponseHeaders();
					foreach (var key in headers.Keys)
					{
						DebugFormat.Log(this, $"Response Header {key} : {headers[key]}");
					}
				}
			}
			compositeDisposable.Dispose();
			return request;

	#if UNITY_EDITOR && ZENJECT
			if (!Application.isPlaying && Zenject.ProjectContext.HasInstance)
				DestroyImmediate(Zenject.ProjectContext.Instance.gameObject);
	#endif
		}

		[Button(ButtonSizes.Medium), GUIColor("AlarmColor"), BoxGroup("Settings"), HorizontalGroup("Settings/1")] public void ClearCookieCache() => UnityWebRequest.ClearCookieCache();
		[Button(ButtonSizes.Medium), GUIColor("AlarmColor"), BoxGroup("Settings"), HorizontalGroup("Settings/1")] public void ClearAccessToken() { Data.accessToken = ""; Save(); }


		public class SaveData
		{
			public string accessToken;
			public string refreshToken;
		}	
	}
}
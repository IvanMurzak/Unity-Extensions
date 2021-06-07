using System;
using System.Collections.Generic;
using UnityEngine.Networking;
using UniRx;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using UnityEngine;
using Cysharp.Threading.Tasks;

public abstract class Request<T>
{
				static		int								id								= 1;
	public		static		int								IncrementalID					=> id++;

	private		static		JsonSerializerSettings			jsonSerializerSettings			= new JsonSerializerSettings()
	{
		MissingMemberHandling	= MissingMemberHandling.Ignore,
		NullValueHandling		= NullValueHandling.Ignore
	};

	public					RequestEvents<T>				Events							{ get; } = new RequestEvents<T>();

	private					string							rootEndpoint;
	private					Dictionary<string, string>		rootHeaders;

	protected				NetworkSO						Network							{ get; private set; }
	protected	abstract	string							Endpoint						{ get; }
	public		abstract	string							RESTMethod						{ get; }

	protected	virtual		Dictionary<string, string>		Headers							=> null;
	public		virtual		string							RequestURL
	{
		get
		{
			var endpoint = Endpoint;
			if (endpoint?.StartsWith("/") ?? false)
				endpoint = endpoint.Substring(1, endpoint.Length - 1);

			var tRootEndPoint = rootEndpoint;
			if (tRootEndPoint.EndsWith("/"))
				tRootEndPoint = tRootEndPoint.Substring(0, tRootEndPoint.Length - 1);

			return $"{tRootEndPoint}/{endpoint}";
		}
	}

	public					UnityWebRequest					LastUnityRequest				{ get; private set; }
	public					string							ResponseRawData					{ get; protected set; } = null;
	public					T								ResponseData					{ get; protected set; } = default(T);

	public					Dictionary<string, string>		GetHeaders()
	{
		var headers = new Dictionary<string, string>();
		if (rootHeaders != null && rootHeaders.Count > 0)
		{
			foreach (var key in rootHeaders.Keys)
				headers[key] = rootHeaders[key];
		}
		var tempHeaders = Headers;
		if (tempHeaders != null && tempHeaders.Count > 0)
		{
			foreach (var key in tempHeaders.Keys)
				headers[key] = tempHeaders[key];
		}
		return headers;
	}

	private		Request		() { }
	public		Request		(NetworkSO network)
	{
		Network			= network;
		rootHeaders		= network.rootHeaders;
		rootEndpoint	= network.rootEndpoint;

		if (rootEndpoint.EndsWith("/"))	rootEndpoint = rootEndpoint.Substring(0, rootEndpoint.Length - 1);
		if (rootHeaders == null)		rootHeaders = new Dictionary<string, string>();
	}
	
	private					Action<V>						TryCatchAction<V>				(Action<V> action, Component component = null)
	{
		return x =>
		{
			try { action(x); }
			catch (Exception e) { DebugFormat.LogException(component, e); }
		};
	}

	public		async		UniTask							SendRequest						()
	{
		UnityWebRequest unityRequest = null;
		try
		{
			unityRequest = CreateUnityWebRequest(RequestURL);

			await PrepeareRequest(unityRequest);
			await unityRequest.SendWebRequest();			
		}
		catch (Exception e) { }
		finally
		{
			try
			{
				await RequestPostprocessing(unityRequest);

				if (unityRequest.result == UnityWebRequest.Result.ConnectionError)	await ProcessNetworkError	(unityRequest);
				if (unityRequest.result == UnityWebRequest.Result.ProtocolError)	await ProcessHttpError		(unityRequest);
				if (unityRequest.isDone && unityRequest.result == UnityWebRequest.Result.Success)
				{
					var json = GetJSON(unityRequest);
					ResponseRawData = json;
					Events.SendSuccessRaw(json);

					var data = FromJSON<T>(unityRequest);
					await OnDataReceived(data);
					Events.SendSuccess(data);
				}
			}
			catch (Exception e2)
			{
				DebugFormat.LogException<Request<T>>(e2);
			}
		}
	}
	public					Request<T>						SubscribeOnSuccess				(Action<T> action, Component component = null)
	{
		var disaposables = Events.OnSuccess.Subscribe(TryCatchAction(action, component));
		if (component != null) disaposables.AddTo(component);
		return this;
	}
	public					Request<T>						SubscribeOnSuccessRaw			(Action<string> action, Component component = null)
	{
		var disaposables = Events.OnSuccessRaw.Subscribe(TryCatchAction(action, component));
		if (component != null) disaposables.AddTo(component);
		return this;
	}
	public					Request<T>						SubscribeOnSerializationError	(Action<string> action, Component component = null)
	{
		var disaposables = Events.OnSerializationError.Subscribe(TryCatchAction(action, component));
		if (component != null) disaposables.AddTo(component);
		return this;
	}
	public					Request<T>						SubscribeOnHttpError			(Action<HttpError> action, Component component = null)
	{
		var disaposables = Events.OnHttpError.Subscribe(TryCatchAction(action, component));
		if (component != null) disaposables.AddTo(component);
		return this;
	}
	public					Request<T>						SubscribeOnNetworkError			(Action<NetworkError> action, Component component = null)
	{
		var disaposables = Events.OnNetworkError.Subscribe(action);
		if (component != null) disaposables.AddTo(component);
		return this;
	}

	protected	abstract	UnityWebRequest					CreateUnityWebRequest			(string endpoint);
	protected	virtual		async UniTask					OnDataReceived					(T data) => ResponseData = data;
	protected	virtual		async UniTask					PrepeareRequest					(UnityWebRequest unityRequest)
	{
		if (rootHeaders != null)
		{
			foreach (var key in rootHeaders.Keys) unityRequest.SetRequestHeader(key, rootHeaders[key]);
		}
		var headers = Headers;
		if (headers != null)
		{
			foreach (var key in headers.Keys) unityRequest.SetRequestHeader(key, headers[key]);
		}
	}
	protected	virtual		async UniTask					RequestPostprocessing			(UnityWebRequest unityRequest)
	{
		LastUnityRequest = unityRequest;
	}
	protected	virtual		async UniTask					ProcessNetworkError				(UnityWebRequest unityRequest)
	{
		ResponseRawData = null;
		Events.SendNetworkError(new NetworkError()
		{
			message			= unityRequest.error
		});
	}
	protected	virtual		async UniTask					ProcessHttpError				(UnityWebRequest unityRequest)
	{
		var json = GetJSON(unityRequest);
		ResponseRawData = json;
		Events.SendHttpErrorRaw(json);

		var errorData = FromJSON<HttpError>(unityRequest);
		if (errorData == null) 
		{
			errorData = new HttpError()
			{
				code	= $"{unityRequest.responseCode}",
				message	= unityRequest.error
			};
		}
		errorData.httpResponseCode = unityRequest.responseCode;
		Events.SendHttpError(errorData);
	}
	protected	virtual		string							GetJSON							(UnityWebRequest unityRequest)
	{
		var bytes = unityRequest.downloadHandler.data;
			bytes = FixBrokenByteArray(bytes);
		return Encoding.UTF8.GetString(bytes);
	}
	protected	virtual		K								FromJSON<K>						(UnityWebRequest unityRequest)
	{
		string json = null;
		try
		{
			json = GetJSON(unityRequest);
			var result = JsonConvert.DeserializeObject<K>(json, jsonSerializerSettings);
			return result;
		}
		catch (Exception e)
		{
			Events.SendSerializationError(e.Message);
			DebugFormat.LogException<Request<T>>(e);

			try					{ DebugFormat.LogError(this, $"JSON ({typeof(K).Name}):\n\n{JsonPrettify(json)}\n"); }
			catch(Exception e2) { DebugFormat.LogError(this, $"JSON ({typeof(K).Name}):\n\n{json}\n"); }
		}
		return default(K);
	}

	private					byte[]							FixBrokenByteArray				(byte[] brokenBytes)
	{
		if (brokenBytes.Length > 3 &&
			brokenBytes[0] == (byte)239 &&
			brokenBytes[1] == (byte)187 &&
			brokenBytes[2] == (byte)191)
		{
			var fixedBytes = brokenBytes.Skip(3).ToArray();
			return FixBrokenByteArray(fixedBytes);
		}
		return brokenBytes;
	}

	public		static		string							JsonPrettify					(string json)
	{
		using (var stringReader = new StringReader(json))
		using (var stringWriter = new StringWriter())
		{
			var jsonReader = new JsonTextReader(stringReader);
			var jsonWriter = new JsonTextWriter(stringWriter) { Formatting = Formatting.Indented };
			jsonWriter.WriteToken(jsonReader);
			return stringWriter.ToString();
		}
	}
}
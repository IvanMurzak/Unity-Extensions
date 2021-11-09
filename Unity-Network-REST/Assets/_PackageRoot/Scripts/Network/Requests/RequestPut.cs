using System;
using System.Text;
using UnityEngine.Networking;
using Newtonsoft.Json;

namespace Network.Extension
{
	public abstract class RequestPut<T1, T2> : Request<T2>
	{
		public		override	string			RESTMethod	=> UnityWebRequest.kHttpVerbPUT;

		public					T1				data;

		public RequestPut(NetworkSO network, T1 data) : base(network)
		{
			this.data = data;
		}

		protected override UnityWebRequest CreateUnityWebRequest(string endpoint)
		{
			var json		= JsonConvert.SerializeObject(data);
			var byteData	= Encoding.UTF8.GetBytes(json);

#if UNITY_EDITOR
#pragma warning disable CS0168 // Variable is declared but never used
			try { DebugFormat.Log<RequestPost<T1, T2>>($"JSON {RESTMethod}:\n\n{JsonPrettify(json)}\n"); }
			catch (Exception e)		{ DebugFormat.Log<RequestPost<T1, T2>>($"JSON {RESTMethod}:\n\n{json}\n"); }
#pragma warning restore CS0168 // Variable is declared but never used
#endif

			return new UnityWebRequest(endpoint)
			{
				method			= RESTMethod,
				uploadHandler	= new UploadHandlerRaw(byteData),
				downloadHandler = new DownloadHandlerBuffer()
			};
		}
	}
}
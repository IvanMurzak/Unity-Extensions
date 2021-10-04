using System.Collections.Generic;
using UnityEngine.Networking;

namespace Network.Extension
{
	public abstract class RequestGet<T> : Request<T>
	{
		public override string RESTMethod => UnityWebRequest.kHttpVerbGET;

		public RequestGet(NetworkSO network) : base(network) { }

		protected override UnityWebRequest CreateUnityWebRequest(string endpoint)
		{
			return UnityWebRequest.Get(endpoint);
		}
	}
}
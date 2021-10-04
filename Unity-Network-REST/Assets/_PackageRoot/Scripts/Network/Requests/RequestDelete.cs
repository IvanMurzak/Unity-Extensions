using UnityEngine.Networking;

namespace Network.Extension
{
	public abstract class RequestDelete<T> : Request<T>
	{
		public override string RESTMethod => UnityWebRequest.kHttpVerbDELETE;

		public RequestDelete(NetworkSO network) : base(network) { }

		protected override UnityWebRequest CreateUnityWebRequest(string endpoint)
		{
			return UnityWebRequest.Delete(endpoint);
		}
	}
}
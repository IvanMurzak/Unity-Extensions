using Newtonsoft.Json;
using UniRx;

namespace Network.Extension
{
	public class DebugRequestEvents<T> : RequestEvents<T>
	{
		const int DEEP = 6;

		public DebugRequestEvents(CompositeDisposable compositeDisposable, Request<T> request, int id) : base()
		{
	#if UNITY_EDITOR
			OnSuccess				.Subscribe(x => DebugFormat.Log			<Request<T>>($"[{request.RESTMethod}] {id} - OnSuccess\n\n{JsonConvert.SerializeObject(x, Formatting.Indented)}\n",			deep: DEEP)).AddTo(compositeDisposable);
			OnSuccessRaw			.Subscribe(x => DebugFormat.Log			<Request<T>>($"[{request.RESTMethod}] {id} - OnSuccessRaw\n\n{Request<T>.JsonPrettify(x)}\n",								deep: DEEP)).AddTo(compositeDisposable);
	#endif
			OnSerializationError	.Subscribe(x => DebugFormat.LogError	<Request<T>>($"[{request.RESTMethod}] {id} - OnSerializationError: {x}",													deep: DEEP)).AddTo(compositeDisposable);
			OnHttpError				.Subscribe(x => DebugFormat.LogError	<Request<T>>($"[{request.RESTMethod}] {id} - OnHttpError\n\n {JsonConvert.SerializeObject(x, Formatting.Indented)}\n",		deep: DEEP)).AddTo(compositeDisposable);
			OnHttpErrorRaw			.Subscribe(x => DebugFormat.LogError	<Request<T>>($"[{request.RESTMethod}] {id} - OnHttpErrorRaw\n\n{x}\n",														deep: DEEP)).AddTo(compositeDisposable);
			OnNetworkError			.Subscribe(x => DebugFormat.LogError	<Request<T>>($"[{request.RESTMethod}] {id} - OnNetworkError\n\n {JsonConvert.SerializeObject(x, Formatting.Indented)}\n",	deep: DEEP)).AddTo(compositeDisposable);
		}
	}
}
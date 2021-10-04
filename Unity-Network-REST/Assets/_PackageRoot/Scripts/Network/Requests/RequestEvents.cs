using System;
using UniRx;

namespace Network.Extension
{
	public class RequestEvents<T>
	{
					Subject<T>					onSuccess				= new Subject<T>();
					Subject<string>				onSuccessRaw			= new Subject<string>();
					Subject<string>				onSerializationError	= new Subject<string>();
					Subject<HttpError>			onHttpError				= new Subject<HttpError>();
					Subject<string>				onHttpErrorRaw			= new Subject<string>();
					Subject<NetworkError>		onNetworkError			= new Subject<NetworkError>();
					Subject<float>				onProgress				= new Subject<float>();
					Subject<bool>				onComplete				= new Subject<bool>();

		public		IObservable<T>				OnSuccess				=> onSuccess;
		public		IObservable<string>			OnSuccessRaw			=> onSuccessRaw;
		public		IObservable<string>			OnSerializationError	=> onSerializationError;
		public		IObservable<HttpError>		OnHttpError				=> onHttpError;
		public		IObservable<string>			OnHttpErrorRaw			=> onHttpErrorRaw;
		public		IObservable<NetworkError>	OnNetworkError			=> onNetworkError;
		public		IObservable<float>			OnProgress				=> onProgress;
		public		IObservable<bool>			OnComplete				=> onComplete;

		public		void						SendSuccess				(T result)						=> onSuccess.OnNext(result);
		public		void						SendSuccessRaw			(string result)					=> onSuccessRaw.OnNext(result);
		public		void						SendSerializationError	(string result)					=> onSerializationError.OnNext(result);
		public		void						SendHttpError			(HttpError result)				=> onHttpError.OnNext(result);
		public		void						SendHttpErrorRaw		(string result)					=> onHttpErrorRaw.OnNext(result);
		public		void						SendNetworkError		(NetworkError result)			=> onNetworkError.OnNext(result);
		public		void						SendProgress			(float result)			=> onProgress.OnNext(result);
		public		void						SendComplete			(bool result)					=> onComplete.OnNext(result);

		public		CompositeDisposable			Subscribe				(RequestEvents<object> events)
		{
			var compositeDisposable = new CompositeDisposable();

			OnSuccess			.Subscribe(x => events.SendSuccess(x));
			OnSuccessRaw		.Subscribe(events.SendSuccessRaw);
			OnSerializationError.Subscribe(events.SendSerializationError);
			OnHttpError			.Subscribe(events.SendHttpError);
			OnHttpErrorRaw		.Subscribe(events.SendHttpErrorRaw);
			OnNetworkError		.Subscribe(events.SendNetworkError);
			OnProgress			.Subscribe(events.SendProgress);
			OnComplete			.Subscribe(events.SendComplete);

			return compositeDisposable;
		}
		public		CompositeDisposable			Subscribe				(RequestEvents<T> events)
		{
			var compositeDisposable = new CompositeDisposable();

			OnSuccess			.Subscribe(x => events.SendSuccess(x));
			OnSuccessRaw		.Subscribe(events.SendSuccessRaw);
			OnSerializationError.Subscribe(events.SendSerializationError);
			OnHttpError			.Subscribe(events.SendHttpError);
			OnHttpErrorRaw		.Subscribe(events.SendHttpErrorRaw);
			OnNetworkError		.Subscribe(events.SendNetworkError);
			OnProgress			.Subscribe(events.SendProgress);
			OnComplete			.Subscribe(events.SendComplete);

			return compositeDisposable;
		}
	}
}
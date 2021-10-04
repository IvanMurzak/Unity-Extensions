using System;

namespace Network.Extension
{
	[Serializable]
	public class HttpError
	{
		public long					httpResponseCode	{ get; set; }
		public string				code				{ get; set; }
		public string				messageCode			{ get; set; }		// could be null
		public string				message				{ get; set; }

		public override string ToString() => $"httpResponseCode=<b>{httpResponseCode}</b>\ncode=<b>{code}</b>\nmessageCode=<b>{messageCode}</b>\nmessage=<b>{message}</b>";
	}

	[Serializable]
	public class NetworkError
	{
		public string				message				{ get; set; }

		public override string ToString() => $"message={message}";
	}
}
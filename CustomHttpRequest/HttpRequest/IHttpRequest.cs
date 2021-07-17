using System.Net;

namespace CustomHttpRequest
{
	public interface IHttpRequest
	{
		string Accept { get; set; }

		string ContentType { get; set; }

		bool KeepAlive { get; set; }

		WebHeaderCollection Headers { get; }

		ICredentials Credentials { get; set; }
	}
}

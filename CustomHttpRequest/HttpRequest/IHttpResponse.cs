using System;
using System.IO;

namespace CustomHttpRequest
{
	public interface IHttpResponse : IDisposable
	{
		Stream GetStream();

		string GetResponseHeader(string headerName);
	}
}

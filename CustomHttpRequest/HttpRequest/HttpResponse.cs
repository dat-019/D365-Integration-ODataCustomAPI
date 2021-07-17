using System;
using System.IO;
using System.Net;

namespace CustomHttpRequest
{
	internal class HttpResponse : IHttpResponse, IDisposable
	{
		private bool _disposed;

		private HttpWebResponse response;

		public HttpResponse(HttpWebResponse response)
		{
			this.response = response;
		}

		protected virtual void Dispose(bool deposing)
		{
			if (!_disposed)
			{
				if (deposing && response != null)
				{
					((IDisposable)response).Dispose();
					response = null;
				}
				_disposed = true;
			}
		}

		public void Dispose()
		{
			Dispose(deposing: true);
		}

		public Stream GetStream()
		{
			return response.GetResponseStream();
		}

		public string GetResponseHeader(string headerName)
		{
			return response.GetResponseHeader(headerName);
		}
	}
}

using System.Net;

namespace CustomHttpRequest
{
	internal class HttpGet : IHttpGet, IHttpRequest
	{
		private readonly HttpWebRequest request;

		public string Accept
		{
			get
			{
				return request.Accept;
			}
			set
			{
				request.Accept = value;
			}
		}

		public string ContentType
		{
			get
			{
				return request.ContentType;
			}
			set
			{
				request.ContentType = value;
			}
		}

		public ICredentials Credentials
		{
			get
			{
				return request.Credentials;
			}
			set
			{
				request.Credentials = value;
			}
		}

		public WebHeaderCollection Headers => request.Headers;

		public bool KeepAlive
		{
			get
			{
				return request.KeepAlive;
			}
			set
			{
				request.KeepAlive = value;
			}
		}

		public HttpGet(HttpWebRequest request)
		{
			this.request = request;
			this.request.Method = "GET";
			this.request.ContentType = "application/json; version=1.0; charset=utf-8";
		}

		public IHttpResponse Do()
		{
			return new HttpResponse((HttpWebResponse)request.GetResponse());
		}
	}
}

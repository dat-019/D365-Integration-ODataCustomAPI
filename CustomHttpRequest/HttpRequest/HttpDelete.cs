using System.IO;
using System.Net;

namespace CustomHttpRequest
{
	internal class HttpDelete : IHttpDelete, IHttpRequest
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

		public HttpDelete(HttpWebRequest request)
		{
			this.request = request;
			this.request.Method = "POST";
			this.request.Headers["X-HTTP-Method"] = "DELETE";
		}

		public IHttpResponse Do(string data)
		{
			if (!string.IsNullOrEmpty(data))
			{
				using (StreamWriter streamWriter = new StreamWriter(request.GetRequestStream()))
				{
					streamWriter.Write(data);
					streamWriter.Flush();
					streamWriter.Close();
				}
			}
			else
			{
				request.ContentLength = 0L;
			}
			return new HttpResponse((HttpWebResponse)request.GetResponse());
		}
	}
}

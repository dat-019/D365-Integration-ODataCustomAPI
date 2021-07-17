using System;
using System.Net;

namespace CustomHttpRequest
{
	public class HttpRequestFactory<T> : IHttpRequestFactory<T>
	{
		public T Create(string url)
		{
			if (typeof(T).IsAssignableFrom(typeof(IHttpPost)))
			{
				return (T)Activator.CreateInstance(Type.GetType("CustomHttpRequest.HttpPost, CustomHttpRequest"), (HttpWebRequest)WebRequest.Create(url));
			}
			if (typeof(T).IsAssignableFrom(typeof(IHttpGet)))
			{
				return (T)Activator.CreateInstance(Type.GetType("CustomHttpRequest.HttpGet, CustomHttpRequest"), (HttpWebRequest)WebRequest.Create(url));
			}
			if (typeof(T).IsAssignableFrom(typeof(IHttpDelete)))
			{
				return (T)Activator.CreateInstance(Type.GetType("CustomHttpRequest.HttpDelete, CustomHttpRequest"), (HttpWebRequest)WebRequest.Create(url));
			}
			if (typeof(T).IsAssignableFrom(typeof(IHttpPut)))
			{
				return (T)Activator.CreateInstance(Type.GetType("CustomHttpRequest.HttpPut, CustomHttpRequest"), (HttpWebRequest)WebRequest.Create(url));
			}
			if (typeof(T).IsAssignableFrom(typeof(IHttpMerge)))
			{
				return (T)Activator.CreateInstance(Type.GetType("CustomHttpRequest.HttpMerge, CustomHttpRequest"), (HttpWebRequest)WebRequest.Create(url));
			}
			if (typeof(T).IsAssignableFrom(typeof(IHttpPatch)))
			{
				return (T)Activator.CreateInstance(Type.GetType("CustomHttpRequest.HttpPatch, CustomHttpRequest"), (HttpWebRequest)WebRequest.Create(url));
			}
			throw new Exception($"No support type '{typeof(T).Name}'");
		}

		public T Create(Uri uri)
		{
			if (typeof(T).IsAssignableFrom(typeof(IHttpPost)))
			{
				return (T)Activator.CreateInstance(Type.GetType("CustomHttpRequest.HttpPost, CustomHttpRequest"), (HttpWebRequest)WebRequest.Create(uri));
			}
			if (typeof(T).IsAssignableFrom(typeof(IHttpGet)))
			{
				return (T)Activator.CreateInstance(Type.GetType("CustomHttpRequest.HttpGet, CustomHttpRequest"), (HttpWebRequest)WebRequest.Create(uri));
			}
			if (typeof(T).IsAssignableFrom(typeof(IHttpDelete)))
			{
				return (T)Activator.CreateInstance(Type.GetType("CustomHttpRequest.HttpDelete, CustomHttpRequest"), (HttpWebRequest)WebRequest.Create(uri));
			}
			if (typeof(T).IsAssignableFrom(typeof(IHttpPut)))
			{
				return (T)Activator.CreateInstance(Type.GetType("CustomHttpRequest.HttpPut, CustomHttpRequest"), (HttpWebRequest)WebRequest.Create(uri));
			}
			if (typeof(T).IsAssignableFrom(typeof(IHttpMerge)))
			{
				return (T)Activator.CreateInstance(Type.GetType("CustomHttpRequest.HttpMerge, CustomHttpRequest"), (HttpWebRequest)WebRequest.Create(uri));
			}
			if (typeof(T).IsAssignableFrom(typeof(IHttpPatch)))
			{
				return (T)Activator.CreateInstance(Type.GetType("CustomHttpRequest.HttpPatch, CustomHttpRequest"), (HttpWebRequest)WebRequest.Create(uri));
			}
			throw new Exception($"No support type '{typeof(T).Name}'");
		}
	}
}

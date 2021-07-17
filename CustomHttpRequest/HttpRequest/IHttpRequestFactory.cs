using System;

namespace CustomHttpRequest
{
	public interface IHttpRequestFactory<T>
	{
		T Create(Uri uri);

		T Create(string url);
	}
}

namespace CustomHttpRequest
{
	public interface IHttpMerge : IHttpRequest
	{
		IHttpResponse Do(string data);
	}
}

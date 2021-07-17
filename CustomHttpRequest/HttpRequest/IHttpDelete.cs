namespace CustomHttpRequest
{
	public interface IHttpDelete : IHttpRequest
	{
		IHttpResponse Do(string data);
	}
}

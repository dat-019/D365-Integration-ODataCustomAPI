namespace CustomHttpRequest
{
	public interface IHttpPut : IHttpRequest
	{
		IHttpResponse Do(string data);
	}
}

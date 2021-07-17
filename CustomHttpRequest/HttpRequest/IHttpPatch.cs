namespace CustomHttpRequest
{
	public interface IHttpPatch : IHttpRequest
	{
		IHttpResponse Do(string data);
	}
}

namespace CustomHttpRequest
{
	public interface IHttpPost : IHttpRequest
	{
		IHttpResponse Do(string data);

		IHttpResponse Do(byte[] data);
	}
}

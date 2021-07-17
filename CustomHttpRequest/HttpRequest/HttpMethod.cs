using System.Runtime.InteropServices;

namespace CustomHttpRequest
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct HttpMethod
	{
		public const string Post = "POST";

		public const string Get = "GET";

		public const string Put = "PUT";

		public const string Patch = "PATCH";
	}
}

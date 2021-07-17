using System.Runtime.InteropServices;

namespace CustomHttpRequest
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct RequestHeader
	{
		public const string Accept = "Accept";

		public const string ContentType = "Content-Type";

		public const string XHttpMethod = "X-HTTP-Method";

		public const string Patch = "PATCH";

		public const string Delete = "DELETE";

		public const string Authorization = "Authorization";
	}
}

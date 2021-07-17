using System.Runtime.InteropServices;

namespace CustomHttpRequest
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct ContentType
	{
		public const string Form = "application/x-www-form-urlencoded";

		public const string Json = "application/json; version=1.0; charset=utf-8";

		public const string Text = "text/plain; charset=utf-8";

		public const string Html = "text/xml; charset=utf-8";

		public const string Xml = "text/xml; charset=utf-8";

		public const string JavaScript = "application/javascript; charset=utf-8";

		public const string Stream = "application/octet-stream";
	}
}

using System.Runtime.InteropServices;

namespace CustomHttpRequest
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct MediaType
	{
		public const string Text = "text/plain";

		public const string Json = "application/json";
	}
}

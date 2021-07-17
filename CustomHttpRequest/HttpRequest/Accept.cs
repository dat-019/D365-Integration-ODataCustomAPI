using System.Runtime.InteropServices;

namespace CustomHttpRequest
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct Accept
	{
		public const string Frm = "application/vnd.frm+json; version=1.0";

		public const string Json = "application/json";
	}
}

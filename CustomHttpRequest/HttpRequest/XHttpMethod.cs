using System.Runtime.InteropServices;

namespace CustomHttpRequest
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct XHttpMethod
	{
		public const string Merge = "MERGE";

		public const string Delete = "DELETE";
	}
}

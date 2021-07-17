using System.Runtime.InteropServices;

namespace CustomHttpRequest
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal struct AssemplyNames
	{
		public const string HttpPost = "CustomHttpRequest.HttpPost, CustomHttpRequest";

		public const string HttpGet = "CustomHttpRequest.HttpGet, CustomHttpRequest";

		public const string HttpDelete = "CustomHttpRequest.HttpDelete, CustomHttpRequest";

		public const string HttpPut = "CustomHttpRequest.HttpPut, CustomHttpRequest";

		public const string HttpMerge = "CustomHttpRequest.HttpMerge, CustomHttpRequest";

		public const string HttpPatch = "CustomHttpRequest.HttpPatch, CustomHttpRequest";
	}
}

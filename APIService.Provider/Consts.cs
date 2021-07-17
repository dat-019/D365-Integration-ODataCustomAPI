using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIService.Provider
{
    public struct HttpMethod
    {
        public const string Post = "POST";
        public const string Get = "GET";
        public const string Put = "PUT";
        public const string Patch = "PATCH";
    }

    public struct XHttpMethod
    {
        public const string Merge = "MERGE";
        public const string Delete = "DELETE";
    }

    public struct RequestHeader
    {
        public const string Accept = "Accept";
        public const string ContentType = "Content-Type";
        public const string XHttpMethod = "X-HTTP-Method";
        public const string Patch = "PATCH";
        public const string Delete = "DELETE";
        public const string Authorization = "Authorization";
    }

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

    public struct MediaType
    {
        public const string Text = "text/plain";
        public const string Json = "application/json";
    }

    public struct Accept
    {
        public const string AcceptHeader = "application/vnd.kor+json; version=1.0";
        public const string Json = "application/json";
    }
}

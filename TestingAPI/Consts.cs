using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIConsumer
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

    public struct FieldType
    {
        public const string Lookup = "EntityReference";
        public const string Memo = "Memo";
        public const string String = "String";
        public const string Decimal = "Decimal";
        public const string Int = "Int";
        public const string Int32 = "Int32";
        public const string Int64 = "Int64";
        public const string Boolean = "Boolean";
        public const string Double = "Double";
        public const string PickList = "OptionSetValue";
        public const string DateTime = "DateTime";
        public const string Money = "Money";
        public const string Guid = "Guid";
        public const string Byte = "Byte[]";
        public const string AliasedValue = "AliasedValue";
        public const string OptionSetValueCollection = "OptionSetValueCollection";
        public const string PartyList = "EntityCollection";
    }
    public struct OptionSet
    {
        public struct Statecode
        {
            public const int Active = 0;
            public const int InActive = 1;
        }
    }
}

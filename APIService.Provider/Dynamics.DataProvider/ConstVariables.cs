using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIService.Provider
{
    public struct ConstVariables
    {
        public struct ApiHeaders
        {
            public struct OdataMaxVersion
            {
                public const string Name = "OData-MaxVersion";
                public const string Value = "4.0";
            }
            public struct ODataVersion
            {
                public const string Name = "OData-Version";
                public const string Value = "4.0";
            }
            public struct Prefer
            {
                public const string Name = "Prefer";
                public const string Value = "odata.include-annotations=\"*\"";
            }

            public struct IfNoneMatch
            {
                public const string Name = "If-None-Match";
                public const string Value = "W\\/\"000000\"";
            }
        }
    }
}

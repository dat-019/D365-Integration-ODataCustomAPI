using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Plugin_DynamicServices
{
    [DataContract]
    public class ErrModel
    {
        [DataMember(Name = "error")]
        public string Error { get; set; }
    }
}

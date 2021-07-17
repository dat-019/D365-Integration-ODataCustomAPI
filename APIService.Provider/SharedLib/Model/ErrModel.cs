using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace APIService.Provider
{
    [DataContract]
    public class ErrModel
    {
        [DataMember(Name = "error")]
        public string Error { get; set; }
    }
}

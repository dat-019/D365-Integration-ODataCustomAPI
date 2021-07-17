using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace APIService.Provider
{
    [DataContract]
    public class Simulator
    {
        [DataMember]
        public string Data{get;set; }
        [DataMember]
        public string Target{get;set;}
    }
}

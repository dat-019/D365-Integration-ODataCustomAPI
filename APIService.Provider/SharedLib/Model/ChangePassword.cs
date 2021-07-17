using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace APIService.Provider
{
    [DataContract]
    public class ChangePassword
    {
        [DataMember]
        public string US{get;set; }
        [DataMember]
        public string PW{get;set; }
        [DataMember]
        public string RPW { get; set; }
    }
}

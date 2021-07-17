using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Plugin_DynamicServices
{
    [DataContract]
    public class SendEmailRegister
    {
        [DataMember]
        public string HoVaTen { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string SanKD { get; set; }
    }
}

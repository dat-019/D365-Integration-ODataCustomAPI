using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace APIService.Provider
{
    [DataContract]
    public class ChangePasswordModel
    {
        [DataMember(Name = "userName")]
        public string UserName { get; set; }
        [DataMember(Name = "passWordOld")]
        public string PassWordOld { get; set; }
        [DataMember(Name = "passWordNew")]
        public string PassWordNew { get; set; }
    }
}

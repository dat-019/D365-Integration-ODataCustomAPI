using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace APIService.Provider
{
    [DataContract]
   public  class CallActionModel
    {
        [DataMember(Name ="entityName")]
        public string EntityName { get; set; }
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "strOwner")]
        public string StrOwner { get; set; }

        [DataMember(Name = "request")]
        public string Request { get; set; }
    }
}

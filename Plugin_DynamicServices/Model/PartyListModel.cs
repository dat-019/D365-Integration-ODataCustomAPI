using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Plugin_DynamicServices
{

    [DataContract]
    public class PartyListModel
    {
        [DataMember(Name = "partyid")]
        public string PartyId { get; set; }
        [DataMember(Name = "partyName")]
        public string PartyName { get; set; }
        [DataMember(Name = "partyLogicalName")]
        public string PartyLogicalName { get; set; }
        [DataMember(Name = "activitypartyid")]
        public string ActivitypartyId { get; set; }
    }
}

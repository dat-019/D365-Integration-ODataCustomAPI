using System.Runtime.Serialization;

namespace Plugin_DynamicServices
{
    [DataContract]
    public class LandingPageModel
    {
        [DataMember(Name = "fullName")]
        public string FullName { get; set; }

        [DataMember(Name = "phoneNumber")]
        public string PhoneNumber { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "source")]
        public string Source { get; set; }

        [DataMember(Name = "ages")]
        public string Ages { get; set; }

        [DataMember(Name = "address")]
        public string Address { get; set; }


        [DataMember(Name = "campaignActivityId")]
        public string CampaignActivityId { get; set; }
    }
}
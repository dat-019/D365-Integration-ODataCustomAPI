using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Plugin_DynamicServices
{
    [DataContract]
    public class TokenModel
    {
        [DataMember(Name = "token_type")]
        public string TokenType { get; set; }
        [DataMember(Name = "expires_in")]
        public string ExpiresIn { get; set; }
        [DataMember(Name = "ext_expires_in")]
        public string ExtExpiresIn { get; set; }
        [DataMember(Name = "expires_on")]
        public string ExpiresOn { get; set; }
        [DataMember(Name = "not_before")]
        public string NotBefore { get; set; }
        [DataMember(Name = "resource")]
        public string Resource { get; set; }
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }
        [DataMember(Name = "scope")]
        public string Scope { get; set; }
        [DataMember(Name = "refresh_token")]
        public string RefreshToken { get; set; }
        public DateTime ExpiresWhen { get; set; }
    }
}

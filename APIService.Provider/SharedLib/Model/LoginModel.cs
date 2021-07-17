using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace APIService.Provider
{
    [DataContract]
    public class LoginModel
    {
        [DataMember(Name ="fullName")]
        public string FullName { get; set; }
        [DataMember(Name = "userName")]
        public string UserName { get; set; }
        [DataMember(Name = "passWord")]
        public string PassWord { get; set; }
        [DataMember(Name = "systemUser")]
        public string SystemUser { get; set; }
        [DataMember(Name = "ttc_sodienthoai")]
        public string SoDienThoai { get; set; }
        [DataMember(Name = "ttc_email")]
        public string Email { get; set; }
        [DataMember(Name = "ttc_ngaysinh")]
        public string NgaySinh { get; set; }
        [DataMember(Name = "entityimage")]
        public string EntityImage { get; set; }
    }
}

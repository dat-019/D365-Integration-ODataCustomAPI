using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin_DynamicServices
{
    public class Employee
    {
        public const string LogicalName = "c30seeds_nhanvien";
        public const string Name = "c30seeds_name";
        public const string TenDangNhap = "c30seeds_username";
        public const string MatKhau = "c30seeds_password";
        public const string Systemuser = "c30seeds_systemuser";
        public const string SoDienThoai = "c30seeds_sodienthoai";
        public const string NgaySinh = "c30seeds_ngaysinh";
        public const string Email = "c30seeds_email";
        public const string Entityimage = "entityimage";
        public const string Statuscode = "statuscode";
        public const string Statecode = "statecode";
        public const string logoutDevices = "c30seeds_logoutdevice";
        public const string UserMail = "c30seeds_useremail";
        public struct StatusCodeValues {
            public const int Active = 0;
            public const int Inactive = 1;
            public const int DaDuyet = 100000001;
        }
    }
}

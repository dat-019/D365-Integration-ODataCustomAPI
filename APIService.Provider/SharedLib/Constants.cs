using System;
using System.Collections.Generic;
using System.Text;

namespace APIService.Provider
{
    public class Constants
    {
        public struct Parameters
        {
            public const string Request = "Request";
            public const string Reponse = "Reponse";
            public const string Result = "Result";
            public const string Target = "Target";
        }
        public struct Message
        {
            public const string Create = "Create";
            public const string Update = "Update";
            public const string Delete = "Delete";
            public const string Retrieve = "Retrieve";
            public const string Associate = "Associate";
            public const string Disassociate = "Disassociate";
            public const string Fetch = "Fetch";
            public const string Login = "Login";
            public const string ChangePassword = "ChangePassword";
            public const string ChangeState = "ChangeState";
            public const string LandingPage = "LandingPage";
        }
        public struct ErrMessage
        {
            // Action Request
            public const string RequiredRequest = "Request parameter is required!";
            public const string ResponMessage = "RequestName: {0} is not support!";
            public const string ResponFieldType = "FieldType: {0} is not support!";
            public const string DisabledUser = "Disabled user";
            public const string RequiredLogout = "Required logout";
        }
        public struct FieldType
        {
            public const string Lookup = "EntityReference";
            public const string Memo = "Memo";
            public const string String = "String";
            public const string Decimal = "Decimal";
            public const string Int = "Int";
            public const string Int32 = "Int32";
            public const string Int64 = "Int64";
            public const string Boolean = "Boolean";
            public const string Double = "Double";
            public const string PickList = "OptionSetValue";
            public const string DateTime = "DateTime";
            public const string Money = "Money";
            public const string Guid = "Guid";
            public const string Byte = "Byte[]";
            public const string AliasedValue = "AliasedValue";
            public const string OptionSetValueCollection = "OptionSetValueCollection";
            public const string PartyList = "EntityCollection";
        }
        public struct OptionSet
        {
            public struct Statecode
            {
                public const int Active = 0;
                public const int InActive = 1;
            }
        }
        public struct ProcessName
        {
            public const string LogoutAllDevices = "c30seeds_LogoutAllDevices";
            public const string ActionSanPhamBookCan = "c30seeds_ActionSanPhamBookCan";
            public const string ActionSanPhamBookCheo = "c30seeds_ActionSanPhamBookCheo";
            public const string ActionSanPhamDatCho = "c30seeds_ActionSanPhamCreatePhieuDatCho";
            public const string ActionDatCocSanPham = "c30seeds_ActionTaoPhieuDatCoc";
            public const string ActionDatCocDatCho = "c30seeds_ActionPhieuDatChoCreatePhieuDatCoc";
            public const string ActionYeuCauHuyCoc = "c30seeds_ActionHuyPhieuDatCoc";
            public const string Qualify = "c30seeds_ActionQualifyKHTiemNang";
            public const string ActionChuyenLienHeChinh = "c30seeds_ActionChuyenLienHeChinh";
            public const string ChiaSeDoanhThuHoaHong = "c30seeds_ActionChiaSeDoanhThuHoaHong";
            public const string CreateAccount = "c30seeds_ActionTaoKhachHangGiaoDich";
            public const string HuyPhieuDatCho = "c30seeds_ActionHuyPhieuDatCho";
            public const string XacNhanThuTien = "c30seeds_ActionXacNhanUuTien";
            public const string Simulator = "c30seeds_ActionSimulatorLaiVay";
            public const string ChietKhauKhuyenMai = "c30seeds_ActionApDungCKKM";
            public const string SendEmailChangePassword = "c30seeds_ActionSendEmailChangePassword";
            public const string GetCSMoiGioi = "c30seeds_ActionGetCSMoiGioi";
            public const string ChiaSerDoanhThuHHCoc72h = "c30seeds_ActionChiaSeDoanhThuHoaHongPhieuCoc72h";
            public const string ChiaSerDoanhThuHHCoc3Ben = "ActionChiaSeDoanhThuHoaHongPhieuCoc3Ben";
            public const string ChiaSerDoanhThuHHCocDXGVNB = "c30seeds_ActionChiaSeDoanhThuHoaHongDeXuatGVNB";
            public const string ChiaSeThongTinDatCho = "c30seeds_ActionChiaSeThongTinDatCho";
            public const string TaoDatChoDatCocDeXuatBanSi = "c30seeds_ActionTaoDatChoDatCocTrenDeXuatBanSi";
            public const string GetCSThanhToan = "c30seeds_ActionGetCSThanhToan";
            public const string BatTrungThongTinKHTN = "c30seeds_BatTrungThongTinTrenKHTN";
        }

        public struct AppSettings
        {
            public const string GrantType = "GrantType";
            public const string UserName = "UserName";
            public const string Password = "Password";
            public const string ApiVersion = "ApiVersion";
            public const string UserManager = "UserManager";
        }
    }
}

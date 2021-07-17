
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIService.Provider
{
    public interface IDataService
    {
        string Fetch(string query, string owner, string userPortalId);
        string Associate(string query, string owner, string userPortalId);
        string Disassociate(string query, string owner, string userPortalId);
        string Delete(string query, string owner, string userPortalId);
        string CallAction(string name, string parameter);
        string ApiFetch(string query);
        string Create(string data, string owner, string userPortalId);
        string Update(string data, string owner, string userPortalId);
        string Login(string data);
        string ChangePassword(string us, string pw, string rpw);
        string ChangeState(string data, string owner, string userPortalId);
        string Booking(string data);
        string BookCheo(string data);
        string DatCho(string data);
        string DatCocSanPham(string data);
        string DatCocDatCho(string data);
        string YeuCauHuyCoc(string data);
        string Qualify(string data);
        string ChuyenLienHeChinh(string data);
        string ChiaSeDoanhThuHoaHong(string data);
        string CreateAccount(string data);
        string HuyPhieuDatCho(string data);
        string XacNhanThuTien(string data);
        string ChietKhauKhuyenMai(string data);
        string Simulator(string data, string target);
        string GetCSMoiGioi(string data);
        string SendEmailChangePassword(string data);
        string SendEmailRegister(string data);
        string ChiaSerDoanhThuHHCoc72h(string data);
        string ChiaSerDoanhThuHHCoc3Ben(string data);
        string ChiaSerDoanhThuHHCocDXGVNB(string data);
        string ChiaSeThongTinDatCho(string data);
        string TaoDatChoDatCocDeXuatBanSi(string data);
        string PushNotification(string data);
        string LandingPage(string data);
        string GetCSThanhToan(string data);
        string BatTrungThongTinKHTN(string data);
        //byte[] ReportFormat(JObject data,  string token);
    }
}

using CustomHttpRequest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace APIService.Provider
{
    public class UserIdentifier : IUserIdentifier<Dictionary<string, string>>
    {

        public Dictionary<string, string> FindUser(string userName, string pass, TokenModel token, ref string outPutMessage)
        {
            Dictionary<string, string> user = null;
            IDataService service = DataServices.CreateDataService(token);
            StringBuilder fetch = new StringBuilder();
            fetch.Append("<fetch top='1' no-lock='true'>");
            fetch.Append("<entity name='c30seeds_nhanvien'>");
            fetch.Append("<attribute name='c30seeds_taikhoan' />");
            fetch.Append("<attribute name='c30seeds_name' />");
            fetch.Append("<attribute name='c30seeds_ngaysinh' />");
            fetch.Append("<attribute name='c30seeds_sodt' />");
            fetch.Append("<attribute name='c30seeds_systemuser' />");
            fetch.Append("<attribute name='c30seeds_nhanvienid' />");
            fetch.Append("<attribute name='c30seeds_san' />");
            fetch.Append("<attribute name='c30seeds_phongkd' />");
            fetch.Append("<attribute name='statuscode' />");
            fetch.Append("<attribute name='statecode' />");
            fetch.Append("<filter type='or'>");
            fetch.Append($"<condition attribute='c30seeds_taikhoan' operator='eq' value='{userName}' />");
            fetch.Append($"<condition attribute='c30seeds_email' operator='eq' value='{userName}' />");
            fetch.Append("</filter>");
            fetch.Append("<link-entity name='systemuser' from='systemuserid' to='c30seeds_systemuser' visible='true' alias='systemuser'>");
            fetch.Append("<attribute name='domainname'/>");
            fetch.Append("<attribute name='businessunitid'/>");
            fetch.Append("<link-entity name='teammembership' from='systemuserid' to='systemuserid' intersect='true'>");
            fetch.Append("<link-entity name='team' from='teamid' to='teamid' alias='team'>");
            fetch.Append("<attribute name='name' />");
            fetch.Append("<attribute name='teamid' />");
            fetch.Append("</link-entity>");
            fetch.Append("</link-entity>");
            fetch.Append("</link-entity>");
            fetch.Append("<link-entity name='c30seeds_restricteduser' from='c30seeds_user' to='c30seeds_nhanvienid' link-type='outer' alias='restricted' >");
            fetch.Append("<attribute name='c30seeds_islogoutdevice' />");
            fetch.Append("<attribute name='c30seeds_isdisabled' />");
            fetch.Append("<attribute name='c30seeds_restricteduserid' />");
            fetch.Append("</link-entity>");
            fetch.Append("</entity>");
            fetch.Append("</fetch>");
            Dictionary<string, string> reponse = Extension.DeSerializeDictionary<Dictionary<string, string>>(service.Fetch(fetch.ToString(), null, string.Empty));
            PageInfor page = Extension.DeSerializeObject<PageInfor>(reponse["Response"]);
            if (page.Results != null && page.Results.Count > 0)
            {
                string disabledName = "restricted.c30seeds_isdisabled";
                string logoutName = "restricted.c30seeds_islogoutdevice";
                RecordModel rc = page.Results.FirstOrDefault();
                
                if (Convert.ToInt32(rc.Field["statecode"].FieldValue.Value) == 1 || (rc.Field.ContainsKey(disabledName) && (bool)rc.Field[disabledName].FieldValue.Value))
                {
                    outPutMessage = "Tài khoản đã bị khóa vui lòng liên hệ administrator!";
                    return null;
                }
                //else if(Convert.ToInt32(rc.Field["statuscode"].FieldValue.Value) != 100000001)
                //{
                //    outPutMessage = "Tài khoản chưa được duyệt!";
                //    return null;
                //}

                if (rc.Field.ContainsKey("systemuser.domainname"))
                {
                    string userDomain = rc.Field["systemuser.domainname"].FieldValue.Value.ToString();
                    if (!string.IsNullOrWhiteSpace(userDomain))
                    {
                        string ClientSecret = ConfigurationManager.AppSettings["ClientSecret"];
                        string ClientId = ConfigurationManager.AppSettings["ClientId"];
                        string loginUrl = ConfigurationManager.AppSettings["LoginUrl"];
                        string Resource = ConfigurationManager.AppSettings["Resource"];
                        string GrantType = ConfigurationManager.AppSettings["GrantType"];
                        IHttpPost post = new HttpRequestFactory<IHttpPost>().Create($"{loginUrl}");
                        post.ContentType = ContentType.Form;
                        string result = string.Empty;
                        DateTime requestOn = DateTime.Now;
                        using (IHttpResponse irep = post.Do($"grant_type={GrantType}&client_id={Uri.EscapeDataString(ClientId)}&client_secret={Uri.EscapeDataString(ClientSecret)}&username={userDomain}&password={pass}&resource={Uri.EscapeDataString(Resource)}"))
                        {
                            using (StreamReader reader = new StreamReader(irep.GetStream()))
                            {
                                string adToken = reader.ReadToEnd();
                                user = new Dictionary<string, string>();
                                //if (rc.Field.ContainsKey("systemuser.businessunitid") && rc.Field["systemuser.businessunitid"].FieldValue != null)
                                //{
                                //    user["businessUnitId"] = rc.Field["systemuser.businessunitid"].FieldValue.Id;
                                //    user["businessUnitName"] = rc.Field["systemuser.businessunitid"].FieldValue.Value.ToString();
                                //}
                                user["businessUnitId"] = rc.Field["c30seeds_san"].FieldValue.Id;
                                user["businessUnitName"] = rc.Field["c30seeds_san"].FieldValue.Value.ToString();
                                user["teamId"] = rc.Field["team.teamid"].FieldValue.Value.ToString();
                                user["teamName"] = rc.Field["team.name"].FieldValue.Value.ToString();
                                user["userPortalId"] = rc.Id;
                                user["userPortalName"] = rc.Field["c30seeds_taikhoan"].FieldValue.Value.ToString();
                                user["childBusinessUnitId"] = rc.Field["c30seeds_phongkd"].FieldValue.Id.ToString();
                                user["childBusinessUnitName"] = rc.Field["c30seeds_phongkd"].FieldValue.Value.ToString();

                                user["userId"] = rc.Field["c30seeds_systemuser"].FieldValue.Id;
                                user["userName"] = rc.Field["c30seeds_systemuser"].FieldValue.Value.ToString();
                                user["domainName"] = userDomain;

                                fetch.Clear();
                                fetch.Append("<fetch>");
                                fetch.Append("<entity name='systemuserroles' >");
                                fetch.Append("<filter type='and' >");
                                fetch.Append($"<condition attribute='systemuserid' operator='eq' value='{user["userId"].ToString()}' />");
                                fetch.Append("</filter>");
                                fetch.Append("<link-entity name='role' from='roleid' to='roleid' link-type='inner' alias='role' >");
                                fetch.Append("<attribute name='name' />");
                                fetch.Append("</link-entity>");
                                fetch.Append("</entity>");
                                fetch.Append("</fetch>");
                                reponse = Extension.DeSerializeDictionary<Dictionary<string, string>>(service.Fetch(fetch.ToString(), null, string.Empty));
                                page = Extension.DeSerializeObject<PageInfor>(reponse["Reponse"]);
                                if (page.Results != null && page.Results.Count > 0)
                                {
                                    user["roles"] = Extension.SerializeObject(page.Results.Where(r => r.Field.ContainsKey("role.name") && r.Field["role.name"] != null).Select(f => f.Field["role.name"].FieldValue.Value).ToArray());
                                }
                                else
                                    user["roles"] = Extension.SerializeObject(new string[] { });

                                fetch.Clear();
                                fetch.Append("<fetch>");
                                fetch.Append("<entity name='businessunit' >");
                                fetch.Append("<attribute name='name' />");
                                fetch.Append("<filter>");
                                fetch.Append($"<condition attribute='parentbusinessunitid' operator='eq' value='{user["businessUnitId"]}' />");
                                fetch.Append("</filter>");
                                fetch.Append("</entity>");
                                fetch.Append("</fetch>");
                                reponse = Extension.DeSerializeDictionary<Dictionary<string, string>>(service.Fetch(fetch.ToString(), null, string.Empty));
                                page = Extension.DeSerializeObject<PageInfor>(reponse["Response"]);
                                if (page.Results != null && page.Results.Count > 0)
                                {
                                    Dictionary<string, string> bus = page.Results.Where(r => r.Field.ContainsKey("name") && r.Field["name"].FieldValue != null && r.Field["name"].FieldValue.Value != null).ToDictionary(b => b.Id, b => b.Field["name"].FieldValue.Value.ToString());
                                    user["childBusinessUnit"] = Extension.SerializeDictionary(bus);
                                }
                                else
                                    user["childBusinessUnit"] = Extension.SerializeDictionary(new Dictionary<string, string>());
                                if ((rc.Field.ContainsKey(logoutName) && (bool)rc.Field[logoutName].FieldValue.Value))
                                {
                                    //RemoveRequiredLogin(service, rc.Id);
                                }
                            }
                        }
                    }
                }
            }
            return user;
        }

        public Dictionary<string, string> FindUser2(string userName, string pass, TokenModel token, ref string outPutMessage)
        {
            Dictionary<string, string> user = null;
            IDataService service = DataServices.CreateDataService(token);
            StringBuilder fetch = new StringBuilder();
            fetch.Append("<fetch top='1' no-lock='true'>");
            fetch.Append("<entity name='c30seeds_nhanvien'>");
            fetch.Append("<attribute name='c30seeds_taikhoan' />");
            fetch.Append("<attribute name='c30seeds_name' />");
            fetch.Append("<attribute name='c30seeds_ngaysinh' />");
            fetch.Append("<attribute name='c30seeds_sodt' />");
            fetch.Append("<attribute name='c30seeds_systemuser' />");
            fetch.Append("<attribute name='c30seeds_nhanvienid' />");
            fetch.Append("<attribute name='c30seeds_san' />");
            fetch.Append("<attribute name='c30seeds_phongkd' />");
            fetch.Append("<attribute name='statuscode' />");
            fetch.Append("<attribute name='statecode' />");
            fetch.Append("<filter type='or'>");
            fetch.Append($"<condition attribute='c30seeds_taikhoan' operator='eq' value='{userName}' />");
            fetch.Append($"<condition attribute='c30seeds_email' operator='eq' value='{userName}' />");
            fetch.Append("</filter>");
            fetch.Append("<link-entity name='systemuser' from='systemuserid' to='c30seeds_systemuser' visible='true' alias='systemuser'>");
            fetch.Append("<attribute name='domainname'/>");
            fetch.Append("<attribute name='businessunitid'/>");
            fetch.Append("<link-entity name='teammembership' from='systemuserid' to='systemuserid' intersect='true'>");
            fetch.Append("<link-entity name='team' from='teamid' to='teamid' alias='team'>");
            fetch.Append("<attribute name='name' />");
            fetch.Append("<attribute name='teamid' />");
            fetch.Append("</link-entity>");
            fetch.Append("</link-entity>");
            fetch.Append("</link-entity>");
            fetch.Append("<link-entity name='c30seeds_restricteduser' from='c30seeds_user' to='c30seeds_nhanvienid' link-type='outer' alias='restricted' >");
            fetch.Append("<attribute name='c30seeds_islogoutdevice' />");
            fetch.Append("<attribute name='c30seeds_isdisabled' />");
            fetch.Append("<attribute name='c30seeds_restricteduserid' />");
            fetch.Append("</link-entity>");
            fetch.Append("</entity>");
            fetch.Append("</fetch>");
            Dictionary<string, string> reponse = Extension.DeSerializeDictionary<Dictionary<string, string>>(service.Fetch(fetch.ToString(), null, string.Empty));
            PageInfor page = Extension.DeSerializeObject<PageInfor>(reponse["Response"]);
            if (page.Results != null && page.Results.Count > 0)
            {
                string disabledName = "restricted.c30seeds_isdisabled";
                string logoutName = "restricted.c30seeds_islogoutdevice";
                RecordModel rc = page.Results.FirstOrDefault();

                if (Convert.ToInt32(rc.Field["statecode"].FieldValue.Value) == 1 || (rc.Field.ContainsKey(disabledName) && (bool)rc.Field[disabledName].FieldValue.Value))
                {
                    outPutMessage = "Tài khoản đã bị khóa vui lòng liên hệ administrator!";
                    return null;
                }
                //else if(Convert.ToInt32(rc.Field["statuscode"].FieldValue.Value) != 100000001)
                //{
                //    outPutMessage = "Tài khoản chưa được duyệt!";
                //    return null;
                //}

                user = new Dictionary<string, string>();
                user["businessUnitId"] = rc.Field["c30seeds_san"].FieldValue.Id;
                user["businessUnitName"] = rc.Field["c30seeds_san"].FieldValue.Value.ToString();
                user["teamId"] = rc.Field["team.teamid"].FieldValue.Value.ToString();
                user["teamName"] = rc.Field["team.name"].FieldValue.Value.ToString();
                user["userPortalId"] = rc.Id;
                user["userPortalName"] = rc.Field["c30seeds_taikhoan"].FieldValue.Value.ToString();
                user["childBusinessUnitId"] = rc.Field["c30seeds_phongkd"].FieldValue.Id.ToString();
                user["childBusinessUnitName"] = rc.Field["c30seeds_phongkd"].FieldValue.Value.ToString();

                user["userId"] = rc.Field["c30seeds_systemuser"].FieldValue.Id;
                user["userName"] = rc.Field["c30seeds_systemuser"].FieldValue.Value.ToString();
                user["domainName"] = rc.Field["systemuser.domainname"].FieldValue.Value.ToString(); ;
            }
            return user;
        }

        public static IUserIdentifier<Dictionary<string, string>> Instance
        {
            get { return new UserIdentifier(); }
        }

        //private void RemoveRequiredLogin(IDataService service, string nhaVienId)
        //{
        //    RecordModel rc = new RecordModel();
        //    rc.Id = nhaVienId;
        //    rc.LogicalName = SharedLib.Entities.NhanVien.LogicalName;
        //    rc.Field = new Dictionary<string, Models.FieldModel>();
        //    rc.Field.Add(SharedLib.Entities.NhanVien.logoutDevices,
        //        new Models.FieldModel()
        //        {
        //            FieldName = SharedLib.Entities.NhanVien.logoutDevices,
        //            FieldValue = new Models.FieldValue() { Value = "false" },
        //            FieldType = "Boolean"
        //        });
        //    service.Update(Extension.SerializeDictionary(rc), string.Empty, string.Empty);
        //}
    }
}

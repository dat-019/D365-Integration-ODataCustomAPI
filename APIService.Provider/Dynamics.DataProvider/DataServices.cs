
using System;
using System.Reflection;
using System.Configuration;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using CustomHttpRequest;

namespace APIService.Provider
{
    public class DataServices : IDataService
    {
        private readonly TokenModel token;
        private readonly string url;
        private readonly string apiVer;
        private readonly string DynamicAction = "cr01f_DynamicServices"; //Name of custom action which is called by external consumer


        private readonly string ClientSecret;
        private readonly string ClientId;
        private readonly string loginUrl;
        private readonly string Resource;
        private readonly string GrantType;
        private readonly string TenentId;
        private readonly string LoginTenentUrl;
        private readonly string ChangePasswordApi;

        private DataServices(TokenModel token, string url, string apiVer)
        {
            this.token = token;
            this.url = url;
            this.apiVer = apiVer;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            ClientSecret = ConfigurationManager.AppSettings["ClientSecret"];
            ClientId = ConfigurationManager.AppSettings["ClientId"];
            loginUrl = ConfigurationManager.AppSettings["LoginUrl"];
            Resource = ConfigurationManager.AppSettings["Resource"];
            GrantType = ConfigurationManager.AppSettings["GrantType"];
            TenentId = ConfigurationManager.AppSettings["tenentId"];
            LoginTenentUrl = ConfigurationManager.AppSettings["loginTenentUrl"];
            ChangePasswordApi = ConfigurationManager.AppSettings["changePasswordApi"];
        }

        public void SetApiHeader<T>(ref T request)
        {
            if (typeof(T).IsAssignableFrom(typeof(IHttpPost)))
            {
                var tmp = (IHttpPost)request;
                tmp.Headers[ConstVariables.ApiHeaders.OdataMaxVersion.Name] = ConstVariables.ApiHeaders.OdataMaxVersion.Value;
                tmp.Headers[ConstVariables.ApiHeaders.ODataVersion.Name] = ConstVariables.ApiHeaders.ODataVersion.Value;
                tmp.Headers[ConstVariables.ApiHeaders.IfNoneMatch.Name] = ConstVariables.ApiHeaders.IfNoneMatch.Value;
                tmp.Headers[ConstVariables.ApiHeaders.Prefer.Name] = ConstVariables.ApiHeaders.Prefer.Value;
                tmp.Headers[RequestHeader.Authorization] = $"{token.TokenType} {token.AccessToken}";
                tmp.KeepAlive = false;
            }
            else if (typeof(T).IsAssignableFrom(typeof(IHttpGet)))
            {
                var tmp = (IHttpGet)request;
                tmp.Headers[ConstVariables.ApiHeaders.OdataMaxVersion.Name] = ConstVariables.ApiHeaders.OdataMaxVersion.Value;
                tmp.Headers[ConstVariables.ApiHeaders.ODataVersion.Name] = ConstVariables.ApiHeaders.ODataVersion.Value;
                tmp.Headers[ConstVariables.ApiHeaders.IfNoneMatch.Name] = ConstVariables.ApiHeaders.IfNoneMatch.Value;
                tmp.Headers[ConstVariables.ApiHeaders.Prefer.Name] = ConstVariables.ApiHeaders.Prefer.Value;
                tmp.Headers[RequestHeader.Authorization] = $"{token.TokenType} {token.AccessToken}";
                tmp.KeepAlive = false;
            }
            else if (typeof(T).IsAssignableFrom(typeof(IHttpDelete)))
            {
                var tmp = (IHttpDelete)request;
                tmp.Headers[ConstVariables.ApiHeaders.OdataMaxVersion.Name] = ConstVariables.ApiHeaders.OdataMaxVersion.Value;
                tmp.Headers[ConstVariables.ApiHeaders.ODataVersion.Name] = ConstVariables.ApiHeaders.ODataVersion.Value;
                tmp.Headers[ConstVariables.ApiHeaders.IfNoneMatch.Name] = ConstVariables.ApiHeaders.IfNoneMatch.Value;
                tmp.Headers[ConstVariables.ApiHeaders.Prefer.Name] = ConstVariables.ApiHeaders.Prefer.Value;
                tmp.Headers[RequestHeader.Authorization] = $"{token.TokenType} {token.AccessToken}";
                tmp.KeepAlive = false;
            }
            else if (typeof(T).IsAssignableFrom(typeof(IHttpPut)))
            {
                var tmp = (IHttpPut)request;
                tmp.Headers[ConstVariables.ApiHeaders.OdataMaxVersion.Name] = ConstVariables.ApiHeaders.OdataMaxVersion.Value;
                tmp.Headers[ConstVariables.ApiHeaders.ODataVersion.Name] = ConstVariables.ApiHeaders.ODataVersion.Value;
                tmp.Headers[ConstVariables.ApiHeaders.IfNoneMatch.Name] = ConstVariables.ApiHeaders.IfNoneMatch.Value;
                tmp.Headers[ConstVariables.ApiHeaders.Prefer.Name] = ConstVariables.ApiHeaders.Prefer.Value;
                tmp.Headers[RequestHeader.Authorization] = $"{token.TokenType} {token.AccessToken}";
                tmp.KeepAlive = false;
            }
            else if (typeof(T).IsAssignableFrom(typeof(IHttpMerge)))
            {
                var tmp = (IHttpMerge)request;
                tmp.Headers[ConstVariables.ApiHeaders.OdataMaxVersion.Name] = ConstVariables.ApiHeaders.OdataMaxVersion.Value;
                tmp.Headers[ConstVariables.ApiHeaders.ODataVersion.Name] = ConstVariables.ApiHeaders.ODataVersion.Value;
                tmp.Headers[ConstVariables.ApiHeaders.IfNoneMatch.Name] = ConstVariables.ApiHeaders.IfNoneMatch.Value;
                tmp.Headers[ConstVariables.ApiHeaders.Prefer.Name] = ConstVariables.ApiHeaders.Prefer.Value;
                tmp.Headers[RequestHeader.Authorization] = $"{token.TokenType} {token.AccessToken}";
                tmp.KeepAlive = false;
            }
            else if (typeof(T).IsAssignableFrom(typeof(IHttpPatch)))
            {
                var tmp = (IHttpPatch)request;
                tmp.Headers[ConstVariables.ApiHeaders.OdataMaxVersion.Name] = ConstVariables.ApiHeaders.OdataMaxVersion.Value;
                tmp.Headers[ConstVariables.ApiHeaders.ODataVersion.Name] = ConstVariables.ApiHeaders.ODataVersion.Value;
                tmp.Headers[ConstVariables.ApiHeaders.IfNoneMatch.Name] = ConstVariables.ApiHeaders.IfNoneMatch.Value;
                tmp.Headers[ConstVariables.ApiHeaders.Prefer.Name] = ConstVariables.ApiHeaders.Prefer.Value;
                tmp.Headers[RequestHeader.Authorization] = $"{token.TokenType} {token.AccessToken}";
                tmp.KeepAlive = false;
            }
            else
                throw new Exception(string.Format("No support type '{0}'", typeof(T).Name));

        }

        #region static

        public static IDataService CreateDataService(TokenModel token)
        {
            /// the types of the constructor parameters, in order
            /// use an empty Type[] array if the constructor takes no parameters
            Type[] paramTypes = new Type[] { typeof(TokenModel), typeof(string), typeof(string) };
            string url = ConfigurationManager.AppSettings["OrgUrl"];
            string apiVer = ConfigurationManager.AppSettings["ApiVersion"];

            /// the values of the constructor parameters, in order
            /// use an empty object[] array if the constructor takes no parameters
            object[] paramValues = new object[] { token, url, apiVer };

            return Construct<IDataService>(typeof(DataServices), paramTypes, paramValues);
        }
        private static T Construct<T>(Type t, Type[] paramTypes, object[] paramValues)
        {
            ConstructorInfo ci = t.GetConstructor(
                BindingFlags.Instance | BindingFlags.NonPublic,
                null, paramTypes, null);

            return (T)ci.Invoke(paramValues);
        }
        #endregion

        public string Fetch(string query, string owner, string userPortalName)
        {
            var parameter = new
            {
                Request = Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    requestName = "Fetch",
                    data = query,
                    owner = string.IsNullOrEmpty(owner) ? "" : owner,
                    userPortal = string.IsNullOrEmpty(userPortalName) ? "" : userPortalName
                })
            };
            return CallAction(DynamicAction, Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
        }

        public string Associate(string query, string owner, string userPortalName)
        {
            var parameter = new
            {
                Request = Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    requestName = "Associate",
                    data = query,
                    owner = string.IsNullOrEmpty(owner) ? "" : owner,
                    userPortal = string.IsNullOrEmpty(userPortalName) ? "" : userPortalName
                })
            };
            return CallAction(DynamicAction, Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
        }

        public string Disassociate(string query, string owner, string userPortalName)
        {
            var parameter = new
            {
                Request = Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    requestName = "Disassociate",
                    data = query,
                    owner = string.IsNullOrEmpty(owner) ? "" : owner,
                    userPortal = string.IsNullOrEmpty(userPortalName) ? "" : userPortalName
                })
            };
            return CallAction(DynamicAction, Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
        }

        public string Delete(string query, string owner, string userPortalName)
        {
            var parameter = new
            {
                Request = Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    requestName = "Delete",
                    data = query,
                    owner = string.IsNullOrEmpty(owner) ? "" : owner,
                    userPortal = string.IsNullOrEmpty(userPortalName) ? "" : userPortalName
                })
            };
            return CallAction(DynamicAction, Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
        }
        public string Create(string data, string owner, string userPortalName)
        {
            var parameter = new
            {
                Request = Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    requestName = "Create",
                    data = data,
                    owner = string.IsNullOrEmpty(owner) ? "" : owner,
                    userPortal = string.IsNullOrEmpty(userPortalName) ? "" : userPortalName
                })
            };
            return CallAction(DynamicAction, Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
        }
        public string Update(string data, string owner, string userPortalName)
        {
            var parameter = new
            {
                Request = Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    requestName = "Update",
                    data = data,
                    owner = owner,
                    userPortal = string.IsNullOrEmpty(userPortalName) ? "" : userPortalName
                })
            };
            return CallAction(DynamicAction, Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
        }

        public string LandingPage(string data)
        {
            var parameter = new
            {
                Request = Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    requestName = Constants.Message.LandingPage,
                    data = data
                })
            };
            return CallAction(DynamicAction, Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
        }

        public string ApiFetch(string query)
        {
            string result = "";
            IHttpGet req = new HttpRequestFactory<IHttpGet>().Create($"{url}{apiVer}{query}");
            SetApiHeader(ref req);
            req.ContentType = ContentType.Json;
            using (IHttpResponse rep = req.Do())
            {
                using (StreamReader reader = new StreamReader(rep.GetStream()))
                {
                    result = reader.ReadToEnd();
                }
            }
            return result;
        }
        public string Login(string data)
        {
            var parameter = new
            {
                Request = Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    requestName = "Login",
                    data = data,
                })
            };
            return CallAction(DynamicAction, Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
        }
        public string ChangePassword(string us, string pw, string rpw)
        {
            TokenModel token_graph = new TokenModel();

            IHttpPost post = new HttpRequestFactory<IHttpPost>().Create(LoginTenentUrl + "/" + TenentId + "/oauth2/v2.0/token");
            post.ContentType = ContentType.Form;
            using (IHttpResponse irep = post.Do($"grant_type={GrantType}&client_id={Uri.EscapeDataString(ClientId)}&client_secret={Uri.EscapeDataString(ClientSecret)}&username={us}&password={pw}&scope=https://graph.microsoft.com/.default"))
            {
                using (StreamReader reader = new StreamReader(irep.GetStream()))
                {
                    token_graph = Extension.DeSerializeObject<TokenModel>(reader.ReadToEnd());
                }
            }

            IHttpPost req = new HttpRequestFactory<IHttpPost>().Create(ChangePasswordApi);
            req.ContentType = ContentType.Json;
            var tmp = (IHttpPost)req;
            tmp.Headers[RequestHeader.Authorization] = $"{token_graph.TokenType} {token_graph.AccessToken}";
            tmp.KeepAlive = false;

            var parameter = new
            {
                currentPassword = pw,
                newPassword = rpw
            };
            using (IHttpResponse rep = req.Do(Newtonsoft.Json.JsonConvert.SerializeObject(parameter)))
            {
                using (StreamReader reader = new StreamReader(rep.GetStream()))
                {
                    return "success";
                }
            }
        }
        public string ChangeState(string data, string owner, string userPortalName)
        {
            var parameter = new
            {
                Request = Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    requestName = "ChangeState",
                    data = data,
                    owner = owner,
                    userPortal = string.IsNullOrEmpty(userPortalName) ? "" : userPortalName
                })
            };
            return CallAction(DynamicAction, Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
        }

        public string Booking(string data)
        {
            CallActionModel callActionModel = Extension.DeSerializeObject<CallActionModel>(data);
            var parameter = new
            {
                strOwner = callActionModel.StrOwner
            };
            return CallActionSpecial(callActionModel, Constants.ProcessName.ActionSanPhamBookCan, Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
        }

        public string BookCheo(string data)
        {
            CallActionModel callActionModel = Extension.DeSerializeObject<CallActionModel>(data);
            var parameter = new
            {
                strOwner = callActionModel.StrOwner
            };
            return CallActionSpecial(callActionModel, Constants.ProcessName.ActionSanPhamBookCheo, Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
        }

        public string DatCho(string data)
        {
            CallActionModel callActionModel = Extension.DeSerializeObject<CallActionModel>(data);
            var parameter = new
            {
                strOwner = callActionModel.StrOwner
            };
            return CallActionSpecial(callActionModel, Constants.ProcessName.ActionSanPhamDatCho, Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
        }

        public string DatCocSanPham(string data)
        {
            CallActionModel callActionModel = Extension.DeSerializeObject<CallActionModel>(data);
            var parameter = new
            {
                strOwner = callActionModel.StrOwner
            };
            return CallActionSpecial(callActionModel, Constants.ProcessName.ActionDatCocSanPham, Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
        }

        public string DatCocDatCho(string data)
        {
            CallActionModel callActionModel = Extension.DeSerializeObject<CallActionModel>(data);
            var parameter = new
            {
                strOwner = callActionModel.StrOwner
            };
            return CallActionSpecial(callActionModel, Constants.ProcessName.ActionDatCocDatCho, Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
        }
        public string Qualify(string data)
        {
            CallActionModel callActionModel = Extension.DeSerializeObject<CallActionModel>(data);
            var parameter = new
            {
                Request = callActionModel.Request,
                strOwner = callActionModel.StrOwner
            };
            return CallActionSpecial(callActionModel, Constants.ProcessName.Qualify, Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
        }
        public string ChuyenLienHeChinh(string data)
        {
            CallActionModel callActionModel = Extension.DeSerializeObject<CallActionModel>(data);
            var parameter = new
            {
                strOwner = callActionModel.StrOwner
            };
            return CallActionSpecial(callActionModel, Constants.ProcessName.ActionChuyenLienHeChinh, Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
        }
        public string ChietKhauKhuyenMai(string data)
        {
            CallActionModel callActionModel = Extension.DeSerializeObject<CallActionModel>(data);
            var parameter = new
            {
                //strOwner = callActionModel.StrOwner
            };
            return CallActionSpecial(callActionModel, Constants.ProcessName.ChietKhauKhuyenMai, Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
        }
        public string ChiaSeDoanhThuHoaHong(string data)
        {
            CallActionModel callActionModel = Extension.DeSerializeObject<CallActionModel>(data);
            var parameter = new
            {
                Request = callActionModel.Request,
            };
            return CallActionSpecial(callActionModel, Constants.ProcessName.ChiaSeDoanhThuHoaHong, Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
        }
        public string ChiaSerDoanhThuHHCoc72h(string data)
        {
            CallActionModel callActionModel = Extension.DeSerializeObject<CallActionModel>(data);
            var parameter = new
            {
                Request = callActionModel.Request,
            };
            return CallActionSpecial(callActionModel, Constants.ProcessName.ChiaSerDoanhThuHHCoc72h, Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
        }

        public string ChiaSerDoanhThuHHCoc3Ben(string data)
        {
            CallActionModel callActionModel = Extension.DeSerializeObject<CallActionModel>(data);
            var parameter = new
            {
                Request = callActionModel.Request,
            };
            return CallActionSpecial(callActionModel, Constants.ProcessName.ChiaSerDoanhThuHHCoc3Ben, Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
        }

        public string ChiaSerDoanhThuHHCocDXGVNB(string data)
        {
            CallActionModel callActionModel = Extension.DeSerializeObject<CallActionModel>(data);
            var parameter = new
            {
                Request = callActionModel.Request,
            };
            return CallActionSpecial(callActionModel, Constants.ProcessName.ChiaSerDoanhThuHHCocDXGVNB, Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
        }

        public string YeuCauHuyCoc(string data)
        {
            CallActionModel callActionModel = Extension.DeSerializeObject<CallActionModel>(data);
            var parameter = new
            {
                strOwner = callActionModel.StrOwner
            };
            return CallActionSpecial(callActionModel, Constants.ProcessName.ActionYeuCauHuyCoc, Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
        }

        public string CreateAccount(string data)
        {
            CallActionModel callActionModel = Extension.DeSerializeObject<CallActionModel>(data);
            var parameter = new
            {
                Request = callActionModel.Request,
            };
            return CallAction(Constants.ProcessName.CreateAccount, Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
        }


        public string HuyPhieuDatCho(string data)
        {
            CallActionModel callActionModel = Extension.DeSerializeObject<CallActionModel>(data);
            var parameter = new
            {
                //strOwner = callActionModel.StrOwner,
            };
            return CallActionSpecial(callActionModel, Constants.ProcessName.HuyPhieuDatCho, Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
        }
        public string XacNhanThuTien(string data)
        {
            CallActionModel callActionModel = Extension.DeSerializeObject<CallActionModel>(data);
            var parameter = new
            {
                //strOwner = callActionModel.StrOwner,
            };
            return CallActionSpecial(callActionModel, Constants.ProcessName.XacNhanThuTien, Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
        }

        public string SendEmailChangePassword(string data)
        {
            var parameter = new
            {
                Data = data
            };
            return CallAction(Constants.ProcessName.SendEmailChangePassword, Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
        }

        public string GetCSMoiGioi(string data)
        {
            var parameter = new
            {
                Data = data
            };
            return CallAction(Constants.ProcessName.GetCSMoiGioi, Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
        }

        public string Simulator(string data, string target)
        {
            var parameter = new
            {
                Data = data,
                Target = target
            };
            return CallAction(Constants.ProcessName.Simulator, Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
        }

        public string ChiaSeThongTinDatCho(string data)
        {
            var parameter = new
            {
                Data = data,
            };
            return CallAction(Constants.ProcessName.ChiaSeThongTinDatCho, Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
        }

        public string TaoDatChoDatCocDeXuatBanSi(string data)
        {
            var parameter = new
            {
                Data = data,
            };
            return CallAction(Constants.ProcessName.TaoDatChoDatCocDeXuatBanSi, Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
        }

        public string GetCSThanhToan(string data)
        {
            var parameter = new
            {
                Data = data,
            };
            return CallAction(Constants.ProcessName.GetCSThanhToan, Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
        }

        public string BatTrungThongTinKHTN(string data)
        {
            var parameter = new
            {
                Request = data,
            };
            return CallAction(Constants.ProcessName.BatTrungThongTinKHTN, Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
        }

        public string PushNotification(string data)
        {
            try
            {
                var result = "";
                string dataConfig = GetConfigNotification();
                PageInfor page = Extension.DeSerializeObject<PageInfor>(dataConfig);
                if (page.Results != null && page.Results.Count > 0)
                {
                    RecordModel rc = page.Results.FirstOrDefault();
                    ModalPushNotification dataModal = Extension.DeSerializeObject<ModalPushNotification>(data);
                    string serveKey = rc.Field["c30seeds_servicekey"].FieldValue.Value.ToString();
                    IHttpPost req = new HttpRequestFactory<IHttpPost>().Create(rc.Field["c30seeds_apisendnoti"].FieldValue.Value.ToString());
                    req.ContentType = ContentType.Json;
                    req.Headers.Add(HttpRequestHeader.Authorization, serveKey);

                    DataNotification objData = new DataNotification();
                    objData.badge = true;
                    objData.sound = "default";
                    objData.priority = "high";
                    objData.content_available = true;
                    objData.notification = new Notification()
                    {
                        title = dataModal.Data.Title,
                        body = dataModal.Data.Message
                    };
                    objData.data = new Params()
                    {
                        logicalName = dataModal.Data.LogicalName,
                        id = dataModal.Data.Id
                    };
                    //Public
                    if (dataModal.ArrayUser == null)
                    {
                        //input: data
                        objData.condition = "'Public' in topics";
                        using (IHttpResponse rep = req.Do(Extension.SerializeDictionary(objData)))
                        {
                            using (StreamReader reader = new StreamReader(rep.GetStream()))
                            {
                                result = reader.ReadToEnd();
                            }
                        }
                    }
                    //BussinesUnit
                    else
                    {
                        //input: data,arrayUser
                        if (dataModal.ArrayUser.Count > 0)
                        {
                            List<string> dataAR = dataModal.ArrayUser;
                            string conditionTopic = string.Join(" in topics && ", dataAR) + " in topics";

                            objData.condition = conditionTopic;
                            using (IHttpResponse rep = req.Do(Extension.SerializeDictionary(objData)))
                            {
                                using (StreamReader reader = new StreamReader(rep.GetStream()))
                                {
                                    result = reader.ReadToEnd();
                                }
                            }
                        }
                        else
                        {
                            result = "Không có sàn kinh doanh hoặc nhân viên được chọn.";
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string SendEmailRegister(string data)
        {
            var info = Extension.DeSerializeObject<SendEmailRegister>(data);
            System.Text.StringBuilder bodyEmail = new System.Text.StringBuilder();
            bodyEmail.Append("Họ và tên: " + info.HoVaTen);
            bodyEmail.Append("\n");
            bodyEmail.Append("Tài khoản (Email): " + info.Email);
            bodyEmail.Append("\n");
            bodyEmail.Append("Sàn kinh doanh: " + info.SanKD);

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("diaockimoanh.cntt@gmail.com");
            mail.To.Add("support@30seeds.com");
            mail.Subject = "Đăng ký tài khoản KOG REAL";
            mail.Body = bodyEmail.ToString();

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new NetworkCredential("diaockimoanh.cntt@gmail.com", "Diaockimoanh@123");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
            return "success";
        }

        public string CallAction(string name, string parameter)
        {
            string json = string.Empty;
            IHttpPost req = new HttpRequestFactory<IHttpPost>().Create($"{url}{apiVer}{name}");
            SetApiHeader(ref req);
            req.ContentType = ContentType.Json;
            using (IHttpResponse rep = req.Do(parameter))
            {
                using (StreamReader reader = new StreamReader(rep.GetStream()))
                {
                    json = reader.ReadToEnd();
                }
            }
            return json;
        }

        public string CallActionSpecial(CallActionModel callActionModel, string actionName, string parameter)
        {
            string json = string.Empty;
            IHttpPost req = new HttpRequestFactory<IHttpPost>().Create($"{url}{apiVer}{callActionModel.EntityName}s({callActionModel.Id})/Microsoft.Dynamics.CRM.{actionName}");
            SetApiHeader(ref req);
            req.ContentType = ContentType.Json;
            using (IHttpResponse rep = req.Do(parameter))
            {
                using (StreamReader reader = new StreamReader(rep.GetStream()))
                {
                    json = reader.ReadToEnd();
                }
            }
            return json;
        }

        public string GetConfigNotification()
        {
            StringBuilder fetch = new StringBuilder();
            fetch.Append("<fetch top='1' >");
            fetch.Append("<entity name='c30seeds_notification' >");
            fetch.Append("<attribute name='c30seeds_apisendnoti' />");
            fetch.Append("<attribute name='c30seeds_servicekey' />");
            fetch.Append("<filter>");
            fetch.Append("<condition attribute='c30seeds_loai' operator='eq' value='100000000' />");
            fetch.Append("</filter>");
            fetch.Append("</entity>");
            fetch.Append("</fetch>");
            Dictionary<string, string> reponse = Extension.DeSerializeDictionary<Dictionary<string, string>>(Fetch(fetch.ToString(), null, string.Empty));
            return reponse["Reponse"];
        }

        //public byte[] ReportFormat(JObject data, string token)
        //{
        //    return ReportServices.FormatJustify(data["orgUniquename"].ToString(), token, data["reportName"].ToString(), data["entityName"].ToString(), data["entityId"].ToString());
        //}

    }
}

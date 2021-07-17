using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace APIService.Provider.CustomAPIController
{
    public class CustomAPIController : ApiController
    {
        protected List<TokenModel> Token
        {
            get
            {
                return AuthenticationVariable.crmAuthenticationProvider.Token;
            }
        }

        protected delegate HttpResponseMessage RequestFunc(HttpRequestMessage request);

        protected HttpResponseMessage ExecuteRequest(HttpRequestMessage request, RequestFunc func)
        {
            if (!ModelState.IsValid)
            {

                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(string.Format("{{\"error\": {{\"code\": \"{0}\",\"message\": {{\"value\": \"{1}\"}}}}}}", (int)HttpStatusCode.InternalServerError, GetModelError(ModelState)), Encoding.UTF8, MediaType.Json),
                    RequestMessage = request
                };
            }
            else
            {
                try
                {
                    return func(request);
                }
                catch (WebException wex)
                {
                    //string g = wex.Response.GetResponseStream().re
                    string message = wex.Message;
                    if (wex.Response == null)
                        return new HttpResponseMessage(HttpStatusCode.Unauthorized)
                        {
                            Content = new StringContent("Please try again!", Encoding.UTF8, MediaType.Json),
                            RequestMessage = request
                        };
                    using (Stream stream = wex.Response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            message = reader.ReadToEnd();
                            reader.Close();
                        }
                        stream.Flush();
                        stream.Close();
                    }
                    if (message.IndexOf($":\"{Constants.ErrMessage.DisabledUser}\"") > 0 || message.IndexOf($":\"{Constants.ErrMessage.RequiredLogout}\"") > 0)
                    {
                        return new HttpResponseMessage(HttpStatusCode.Unauthorized)
                        {
                            Content = new StringContent(message, Encoding.UTF8, MediaType.Json),
                            RequestMessage = request
                        };
                    }
                    else if (message.Contains("invalid username or password"))
                        return new HttpResponseMessage(HttpStatusCode.Unauthorized)
                        {
                            Content = new StringContent("Mật khẩu cũ không chính xác!", Encoding.UTF8, MediaType.Json),
                            RequestMessage = request
                        };
                    else if (message.Contains("specified password does not comply with password complexity requirements"))
                        return new HttpResponseMessage(HttpStatusCode.Unauthorized)
                        {
                            Content = new StringContent("Vui lòng nhập mật khẩu đúng với các điều kiện sau: Tối thiểu 8 kí tự. Có số, chữ và kí tự đặc biệt", Encoding.UTF8, MediaType.Json),
                            RequestMessage = request
                        };
                    else
                        return new HttpResponseMessage(((HttpWebResponse)wex.Response).StatusCode)
                        {
                            Content = new StringContent(message, Encoding.UTF8, MediaType.Json),
                            RequestMessage = request
                        };
                }
                catch (Exception ex)
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent(string.Format("{{\"error\": {{\"code\": \"{0}\",\"message\": {{\"value\": \"{1}\"}}}}}}", (int)HttpStatusCode.InternalServerError, ex.Message), Encoding.UTF8, MediaType.Json),
                        RequestMessage = request
                    };
                }
            }

        }

        private string GetModelError(ModelStateDictionary modelState)
        {
            StringBuilder sb = new StringBuilder();
            foreach (ModelState m in modelState.Values)
            {
                ModelError me = m.Errors.FirstOrDefault();
                if (me != null)
                {
                    sb.AppendLine(me.ErrorMessage);
                }
            }
            return sb.ToString();
        }

    }
}

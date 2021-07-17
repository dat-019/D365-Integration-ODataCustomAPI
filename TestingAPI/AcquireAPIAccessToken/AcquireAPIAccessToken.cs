using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using System.Net.Http.Formatting;

namespace APIConsumer
{
    public static class AcquireAPIAccessToken
    {
        public static string GetAPIToken()
        {
            var accessTokenString = "";

            //Ref: https://www.c-sharpcorner.com/UploadFile/ff2f08/token-based-authentication-using-Asp-Net-web-api-owin-and-i/

            string baseAddress = "https://localhost:44333";
            using (var client = new HttpClient())
            {
               var form = new Dictionary<string, string>
               {
                   { "grant_type", "password"},
                   { "username", "tgd@diaockimoanh.com.vn"},
                   { "password", "kog@30seeds"},
                   //{ "accept", "application/vnd.kor+json; version=1.0"},
               };
                var tokenResponse = client.PostAsync(baseAddress + "/api/token", new FormUrlEncodedContent(form)).Result;
                //var token = tokenResponse.Content.ReadAsStringAsync().Result;  
                var token = tokenResponse.Content.ReadAsAsync<Token>(new[] { new JsonMediaTypeFormatter() }).Result;
                if (string.IsNullOrEmpty(token.Error))
                {
                    Console.WriteLine("Token issued is: {0}", token.AccessToken);
                    accessTokenString = token.AccessToken;
                }
                else
                {
                    Console.WriteLine("Error : {0}", token.Error);
                }
            }
            return accessTokenString;
        }
    }
}

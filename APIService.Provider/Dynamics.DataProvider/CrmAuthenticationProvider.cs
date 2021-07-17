using CustomHttpRequest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace APIService.Provider
{
    public class CrmAuthenticationProvider : ICrmAuthenticationProvider
    {
        private readonly string ClientSecret;
        private readonly string ClientId;
        private readonly string loginUrl;
        private readonly string Resource;
        private readonly string GrantType;
        private readonly string[] uns;
        private readonly string[] pws;
        private int counter = 0;
        public static readonly object lockSyn = "";
        private List<TokenModel> tokens;

        public List<TokenModel> Token
        {
            get
            {
                if (tokens != null && tokens.Count > 0)
                {
                    if (tokens.FirstOrDefault().ExpiresWhen <= DateTime.Now)
                        RefreshToken();
                }
                return tokens;
            }
        }

        private CrmAuthenticationProvider()
        {
            ClientSecret = ConfigurationManager.AppSettings["ClientSecret"];
            ClientId = ConfigurationManager.AppSettings["ClientId"];
            loginUrl = ConfigurationManager.AppSettings["LoginUrl"];
            Resource = ConfigurationManager.AppSettings["Resource"];
            GrantType = ConfigurationManager.AppSettings["GrantType"];

            uns = ConfigurationManager.AppSettings["UserName"].Split('|');
            pws = ConfigurationManager.AppSettings["Password"].Split('|');
        }

        public static ICrmAuthenticationProvider RegisterCrmAuthentication()
        {
            return Activator.CreateInstance(typeof(CrmAuthenticationProvider), true) as ICrmAuthenticationProvider;
        }

        public void AuthenticateTo365()
        {
            lock (lockSyn)
            {
                counter = 0;
                tokens = _GetToken();
            }
        }

        public void RefreshToken()
        {
            lock (lockSyn)
            {
                counter = 0;
                _RefreshToken(ref tokens);
            }
        }

        private List<TokenModel> _GetToken()
        {
            counter += 1;
            if (counter > 3)
                return null;
            List<TokenModel> tokens = new List<TokenModel>();
            try
            {
                for (int i = 0; i < uns.Length; i++)
                {
                    IHttpPost post = new HttpRequestFactory<IHttpPost>().Create($"{loginUrl}");
                    post.ContentType = ContentType.Form;
                    string result = string.Empty;
                    DateTime requestOn = DateTime.Now;
                    using (IHttpResponse irep = post.Do($"grant_type={GrantType}&client_id={Uri.EscapeDataString(ClientId)}&client_secret={Uri.EscapeDataString(ClientSecret)}&username={uns[i]}&password={pws[i]}&resource={Uri.EscapeDataString(Resource)}"))
                    {
                        using (StreamReader reader = new StreamReader(irep.GetStream()))
                        {
                            TokenModel tmp = Extension.DeSerializeObject<TokenModel>(reader.ReadToEnd());
                            tmp.ExpiresWhen = requestOn.AddMinutes(-5).AddSeconds(double.Parse(tmp.ExpiresIn));
                            tokens.Add(tmp);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                tokens = _GetToken();
            }

            return tokens;
        }

        private void _RefreshToken(ref List<TokenModel> tokens)
        {
            counter += 1;
            if (counter > 3)
            {
                tokens = new List<TokenModel>();
                return;
            }
            try
            {
                tokens = new List<TokenModel>();
                DateTime requestOn = DateTime.Now;

                for (int i = 0; i < uns.Length; i++)
                {
                    IHttpPost post = new HttpRequestFactory<IHttpPost>().Create($"{loginUrl}");
                    post.ContentType = ContentType.Form;
                    string result = string.Empty;
                    using (IHttpResponse irep = post.Do($"grant_type={GrantType}&client_id={Uri.EscapeDataString(ClientId)}&client_secret={Uri.EscapeDataString(ClientSecret)}&username={uns[i]}&password={pws[i]}&resource={Uri.EscapeDataString(Resource)}"))
                    {
                        using (StreamReader reader = new StreamReader(irep.GetStream()))
                        {
                            TokenModel tmp = Extension.DeSerializeObject<TokenModel>(reader.ReadToEnd());
                            tmp.ExpiresWhen = requestOn.AddMinutes(-5).AddSeconds(double.Parse(tmp.ExpiresIn));
                            tokens.Add(tmp);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _RefreshToken(ref tokens);
            }
        }
    }
}

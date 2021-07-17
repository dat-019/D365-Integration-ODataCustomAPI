using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Owin.Security;
using System.Net;
using System.IO;
using System.Configuration;
using APIService.Provider;

namespace APIService.Provider
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            #region old code
            //if (context.Request.Accept != Accept.AcceptHeader)
            //{
            //    context.Rejected();
            //    context.SetError("invalid request");
            //}
            //context.Validated();
            #endregion

            //new code
            context.Validated();

        }
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            #region old code

            //old code
            //var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            //if (context.Request.Accept != Accept.AcceptHeader)
            //{
            //    context.Rejected();
            //    context.SetError("invalid request");
            //}
            //else
            //{
            //    string errorMessage = "Invalid user name or password!";
            //    try
            //    {
            //        string userMessage = string.Empty;
            //        IUserIdentifier<Dictionary<string, string>> userIdentifier = UserIdentifier.Instance;
            //        Dictionary<string, string> user = userIdentifier.FindUser(context.UserName, context.Password, Extension.RandomToken(AuthenticationVariable.crmAuthenticationProvider.Token), ref userMessage);
            //        if (user != null)
            //        {
            //            errorMessage = string.Empty;
            //            identity.AddClaim(new Claim("User", context.UserName));
            //            identity.AddClaim(new Claim("UserId", user["userId"]));
            //            identity.AddClaim(new Claim("UserName", user["userName"]));
            //            identity.AddClaim(new Claim("UserPortalName", user["userPortalName"]));
            //            var props = new AuthenticationProperties(user);
            //            var ticket = new AuthenticationTicket(identity, props);
            //            context.Validated(ticket);
            //        }
            //        else if (!string.IsNullOrWhiteSpace(userMessage))
            //            errorMessage = userMessage;
            //    }
            //    catch (WebException wex)
            //    {
            //        using (Stream stream = wex.Response.GetResponseStream())
            //        {
            //            using (StreamReader reader = new StreamReader(stream))
            //            {
            //                string adError = reader.ReadToEnd();
            //                if (!adError.ToLower().Contains("invalid username or password"))
            //                    errorMessage = adError;
            //                reader.Close();
            //            }
            //            stream.Flush();
            //            stream.Close();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        errorMessage = ex.Message;
            //    }
            //    if (!string.IsNullOrWhiteSpace(errorMessage))
            //    {
            //        context.Rejected();
            //        context.SetError("invalid_grant", errorMessage);
            //    }
            //}
            //------------------------------------------------------------------
            #endregion

            //new custom code
            string errorMessage = "Invalid user name or password!";
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            try
            {
                string userMessage = string.Empty;
                IUserIdentifier<Dictionary<string, string>> userIdentifier = UserIdentifier.Instance;
                Dictionary<string, string> user = userIdentifier.FindUser2(context.UserName, context.Password, Extension.RandomToken(AuthenticationVariable.crmAuthenticationProvider.Token), ref userMessage);
                if (user != null)
                {
                    errorMessage = string.Empty;
                    identity.AddClaim(new Claim("User", context.UserName));
                    identity.AddClaim(new Claim("UserId", user["userId"]));
                    identity.AddClaim(new Claim("UserName", user["userName"]));
                    identity.AddClaim(new Claim("UserPortalName", user["userPortalName"]));
                    var props = new AuthenticationProperties(user);
                    var ticket = new AuthenticationTicket(identity, props);
                    context.Validated(ticket);
                }
                else if (!string.IsNullOrWhiteSpace(userMessage))
                    errorMessage = userMessage;
            }
            catch (WebException wex)
            {
                using (Stream stream = wex.Response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string adError = reader.ReadToEnd();
                        if (!adError.ToLower().Contains("invalid username or password"))
                            errorMessage = adError;
                        reader.Close();
                    }
                    stream.Flush();
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                context.Rejected();
                context.SetError("invalid_grant", errorMessage);
            }
            //end new code

        }
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

    }
}
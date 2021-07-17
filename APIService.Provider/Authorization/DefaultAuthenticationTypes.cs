using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIService.Provider
{
    public static class DefaultAuthenticationTypes
    {
        //
        // Summary:
        //     Default value for the main application cookie used by UseSignInCookies
        public const string ApplicationCookie = "ApplicationCookie";
        //
        // Summary:
        //     Default value used by the UseOAuthBearerTokens method
        public const string ExternalBearer = "ExternalBearer";
        //
        // Summary:
        //     Default value used for the ExternalSignInAuthenticationType configured by UseSignInCookies
        public const string ExternalCookie = "ExternalCookie";
        //
        // Summary:
        //     Default value for authentication type used for two factor partial sign in
        public const string TwoFactorCookie = "TwoFactorCookie";
        //
        // Summary:
        //     Default value for authentication type used for two factor remember browser
        public const string TwoFactorRememberBrowserCookie = "TwoFactorRememberBrowser";
    }
}
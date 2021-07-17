using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIService.Provider
{
    public static class AuthenticationVariable
    {
        public static ICrmAuthenticationProvider crmAuthenticationProvider = CrmAuthenticationProvider.RegisterCrmAuthentication();
    }
}
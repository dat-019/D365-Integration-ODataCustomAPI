using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIService.Provider
{
    public interface ICrmAuthenticationProvider
    {
        List<TokenModel> Token { get; }
        void AuthenticateTo365();
        void RefreshToken();
    }
}

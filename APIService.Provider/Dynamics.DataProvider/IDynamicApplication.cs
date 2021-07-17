using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIService.Provider
{
    public interface IDynamicApplication
    {
        ICrmAuthenticationProvider CrmAuthenticationProvider { set; }
        List<TokenModel> Token { get; }

        void GetToken();
        void RefreshToken();
    }
}

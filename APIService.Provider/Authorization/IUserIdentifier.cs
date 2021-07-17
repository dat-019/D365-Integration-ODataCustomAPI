using System.Threading.Tasks;

namespace APIService.Provider
{
    public interface IUserIdentifier<T>
    {
        T FindUser(string userName, string pass,TokenModel token,ref string outMessage);

        T FindUser2(string userName, string pass, TokenModel token, ref string outMessage);
    }
}

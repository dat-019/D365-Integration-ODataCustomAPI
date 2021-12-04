using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace APIConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            //get api
            string apiAccessToken = AcquireAPIAccessToken.GetAPIToken(); //acquire the API access token

            IConsumeCustomAPI consumeCustomAPI = new ConsumeCustomAPI(apiAccessToken);
            
            consumeCustomAPI.ConsumeFetchCustomAPI(); //build fetch xml

            consumeCustomAPI.ConsumeCreateRecordCustomAPI(); //Create new record

        }
    }
}

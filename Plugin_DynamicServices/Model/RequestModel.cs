using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin_DynamicServices
{ 
    public class RequestModel
    {
        public string requestName
        {
            get; set;
        }
        public string data
        {
            get; set;
        }
        public string owner
        {
            get; set;
        }

        public string userPortal
        {
            get;set;
        }
    }
}

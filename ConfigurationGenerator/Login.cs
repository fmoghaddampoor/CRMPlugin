using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationGenerator
{
    public class Login
    {
        public System.Uri Uri { set; get; }
        public ClientCredentials Credentials { set; get; }
    }
}

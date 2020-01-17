using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationGenerator
{
    public class CRMAdminLogin
    {
        public System.Uri GetUri()
        {
            return new System.Uri(GetURL());
        }
        public string GetUser()
        {
            return ConfigurationManager.ConnectionStrings["user"].ToString();
        }
        public string GetURL()
        {
            return ConfigurationManager.ConnectionStrings["url"].ConnectionString;
        }
        public string GetPassword()
        {
            return ConfigurationManager.ConnectionStrings["password"].ConnectionString;
        }
        public ClientCredentials GetClientCredentials()
        {
            ClientCredentials clientCredentials = new ClientCredentials();
            clientCredentials.UserName.UserName = GetUser();
            clientCredentials.UserName.Password = GetPassword();
            return clientCredentials;
        }
    }
}

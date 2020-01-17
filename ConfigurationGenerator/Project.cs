using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationGenerator
{
    public sealed class Project
    {
        private readonly String _value;

        public static readonly Project regUser = new Project("user");
        public static readonly Project regPassword = new Project("password");
        public static readonly Project regUrl = new Project("url");
        public static readonly Project Company = new Project("Reply");
        public static readonly Project PrincipalDeveloper = new Project("ulixe");
        public static readonly Project regSelectedCSVFile = new Project("selectedCSVFile");
        private Project(string value)
        {
            this._value = value;
        }
        public override String ToString()
        {
            return _value;
        }
      
    }
}

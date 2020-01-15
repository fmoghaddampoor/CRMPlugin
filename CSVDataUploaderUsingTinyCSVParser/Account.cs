using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVDataUploader
{
    public class Account
    {
        public string Name { get; set; }

        public string PrimaryContactId { get; set; }

        public string Telephone1 { get; set; }

        public static int GetPropertyCount()
        {
            return typeof(Account).GetProperties().Count();
        }
    }
}

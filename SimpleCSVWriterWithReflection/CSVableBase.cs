using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVManager
{
    public abstract class CSVableBase
    {
        public virtual string ToCSV()
        {
            string output = "";
            var properties = GetType().GetProperties();
            for (var i = 0; i < properties.Length; i++)
            {
                output += properties[i].GetValue(this).ToString();
                if (i != properties.Length - 1)
                {
                    output += ",";
                }
            }
            return output;
        }
    }
}

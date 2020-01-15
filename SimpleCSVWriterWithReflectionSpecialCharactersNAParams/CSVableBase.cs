using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVManager
{
    public abstract class CSVableBase
    {
        public virtual string ToCSV(string[] propertyNames, bool isIgnore)
        {
            string output = "";
            bool isFirstPropertyWritten = false;
            var properties = GetType().GetProperties();
            for (var i = 0; i < properties.Length; i++)
            {
                if (isIgnore)
                {
                    if (!propertyNames.Contains(properties[i].Name))
                    {
                        if (isFirstPropertyWritten)
                        {
                            output += ",";
                        }
                        output += PreProcess(properties[i].GetValue(this).ToString());

                        if (!isFirstPropertyWritten)
                        {
                            isFirstPropertyWritten = true;
                        }
                    }
                }
                else
                {
                    if (propertyNames.Contains(properties[i].Name))
                    {
                        if (isFirstPropertyWritten)
                        {
                            output += ",";
                        }
                        output += PreProcess(properties[i].GetValue(this).ToString());

                        if (!isFirstPropertyWritten)
                        {
                            isFirstPropertyWritten = true;
                        }
                    }
                }
            }
            return output;
        }

        public virtual string ToCSV(int[] propertyIndexes, bool isIgnore)
        {
            string output = "";
            bool isFirstPropertyWritten = false;
            var properties = GetType().GetProperties();
            for (var i = 0; i < properties.Length; i++)
            {
                if (isIgnore)
                {
                    if (!propertyIndexes.Contains(i))
                    {
                        if (isFirstPropertyWritten)
                        {
                            output += ",";
                        }
                        output += PreProcess(properties[i].GetValue(this).ToString());
                        if (!isFirstPropertyWritten)
                        {
                            isFirstPropertyWritten = true;
                        }
                    }
                }
                else
                {
                    if (propertyIndexes.Contains(i))
                    {
                        if (isFirstPropertyWritten)
                        {
                            output += ",";
                        }
                        output += PreProcess(properties[i].GetValue(this).ToString());
                        if (!isFirstPropertyWritten)
                        {
                            isFirstPropertyWritten = true;
                        }
                    }
                }

            }
            return output;
        }
        private string PreProcess(string input)
        {
            input = input.Replace('ı', 'i')
                .Replace('ç', 'c')
                .Replace('ö', 'o')
                .Replace('ş', 's')
                .Replace('ü', 'u')
                .Replace('ğ', 'g')
                .Replace('İ', 'I')
                .Replace('Ç', 'C')
                .Replace('Ö', 'O')
                .Replace('Ş', 'S')
                .Replace('Ü', 'U')
                .Replace('Ğ', 'G')
                .Replace("\"", "\"\"")
                .Trim();
            if (input.Contains(","))
            {
                input = "\"" + input + "\"";
            }
            return input;
        }
    }
}

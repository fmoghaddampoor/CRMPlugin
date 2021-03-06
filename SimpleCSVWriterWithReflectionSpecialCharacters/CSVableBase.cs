﻿using System;
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
                output += PreProcess(properties[i].GetValue(this).ToString());
                if (i != properties.Length - 1)
                {
                    output += ",";
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVManager
{
    public abstract class CSVableBase
    {
        public virtual string ToCsv()
        {
            string output = "";
            var properties = GetType().GetProperties();
            for (var i = 0; i < properties.Length; i++)
            {
                if (properties[i].PropertyType.IsSubclassOf(typeof(CSVableBase)))
                {
                    var m = properties[i].PropertyType.GetMethod("ToCsv", new Type[0]);
                    output += m.Invoke(properties[i].GetValue(this), new object[0]);
                }
                else
                {
                    output += PreProcess(properties[i].GetValue(this).ToString());
                }
                if (i != properties.Length - 1)
                {
                    output += ",";
                }
            }
            return output;
        }

        public virtual string ToCsv(string[] propertyNames, bool isIgnore)
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

                        if (properties[i].PropertyType
                            .IsSubclassOf(typeof(CSVableBase)))
                        {
                            var m = properties[i].PropertyType
                            .GetMethod("ToCsv", new Type[0]);
                            output += m.Invoke(properties[i].GetValue(this),
                                                new object[0]);
                        }
                        else
                        {
                            output += PreProcess(properties[i]
                                        .GetValue(this).ToString());
                        }

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

                        if (properties[i].PropertyType
                        .IsSubclassOf(typeof(CSVableBase)))
                        {
                            var m = properties[i].PropertyType
                                    .GetMethod("ToCsv", new Type[0]);
                            output += m.Invoke(properties[i].GetValue(this),
                                                new object[0]);
                        }
                        else
                        {
                            output += PreProcess(properties[i]
                                        .GetValue(this).ToString());
                        }

                        if (!isFirstPropertyWritten)
                        {
                            isFirstPropertyWritten = true;
                        }
                    }
                }
            }

            return output;
        }

        public virtual string ToCsv(int[] propertyIndexes, bool isIgnore)
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

                        if (properties[i].PropertyType
                            .IsSubclassOf(typeof(CSVableBase)))
                        {
                            var m = properties[i].PropertyType
                                    .GetMethod("ToCsv", new Type[0]);
                            output += m.Invoke(properties[i].GetValue(this),
                                                new object[0]);
                        }
                        else
                        {
                            output += PreProcess(properties[i]
                                        .GetValue(this).ToString());
                        }

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

                        if (properties[i].PropertyType
                            .IsSubclassOf(typeof(CSVableBase)))
                        {
                            var m = properties[i].PropertyType
                                    .GetMethod("ToCsv", new Type[0]);
                            output += m.Invoke(properties[i].GetValue(this),
                                                new object[0]);
                        }
                        else
                        {
                            output += PreProcess(properties[i]
                                        .GetValue(this).ToString());
                        }

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

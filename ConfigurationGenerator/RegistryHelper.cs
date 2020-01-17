using Microsoft.Win32;
using System;

namespace ConfigurationGenerator
{
    public class RegistryHelper
    {
        private readonly string _swName ;
        private readonly string _companyName ;
        public RegistryHelper(string companyName, string swName)
        {
            _swName = swName;
            _companyName = companyName;
        }

        /// <summary>
        /// Store a value in registry
        /// </summary>
        /// <param name="KeyName"></param>
        /// <param name="value"></param>
        public void AddValueToRegistry(string KeyName, string value)
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software", true);
            //
            key.CreateSubKey(_companyName);
            key = key.OpenSubKey(_companyName, true);
            //
            key.CreateSubKey(_swName);
            key = key.OpenSubKey(_swName, true);
            //
            key.SetValue(KeyName, value);
        }

        public string ReadValueFromRegistry(string KeyName)
        {
            string result = "";
            try
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\" + _companyName + "\\" + _swName);
                if (key != null)
                {
                    Object o = key.GetValue(KeyName);
                    if (o != null)
                    {
                        result = o.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
    }
}

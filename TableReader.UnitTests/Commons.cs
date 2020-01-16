using System;
using System.IO;
using System.Reflection;

namespace TableReader.UnitTests
{
    public static class Commons
    {
        public static string Sheet1 = "Foglio1";
        public static string GetAssemblyDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            System.UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
        public static string GetTestFilesDirectory()
        {
           return Path.Combine(GetAssemblyDirectory(), "TestFile");
        }
    }
}

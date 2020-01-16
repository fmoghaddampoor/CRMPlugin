using System;
using System.Data;
using System.IO;
using System.Reflection;

namespace TestOpenXml
{
    class Program
    {
        static void Main(string[] args)
        {
            OpenXML.ExcelReader excelReader = new OpenXML.ExcelReader();
            
            excelReader.Print(XLSMFilePath, "Foglio1");
            Console.ReadKey();
            excelReader.Print(XLSXFilePath, "Foglio1");
            Console.ReadKey();

            excelReader.Print(@"c:\", "Foglio1");
            Console.ReadKey();

            excelReader.Print(CiaoFilePath, "Foglio1");
            Console.ReadKey();
        }

        private static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                System.UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        private static string TestFilesDirectory
        {
            get
            {               
                return Path.Combine(AssemblyDirectory, "TestFile");
            }
        }

        private static string XLSMFilePath
        {
            get
            {
                return Path.Combine(TestFilesDirectory, "Test.xlsm");
            }
        }

        private static string XLSXFilePath
        {
            get
            {
                return Path.Combine(TestFilesDirectory, "Test.xlsx");
            }
        }
        private static string CiaoFilePath
        {
            get
            {
                return Path.Combine(TestFilesDirectory, "Test.ciao");
            }
        }
    }

}

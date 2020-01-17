using System;
using System.IO;
using System.Reflection;

namespace TestTableReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var excelReader = new TableReader.XMLReader(XLSMFilePath, "Foglio1");
            excelReader.Print();
            Console.ReadKey();

            excelReader = new TableReader.XMLReader(XLSXFilePath, "Foglio1");
            excelReader.Print();
            Console.ReadKey();

            excelReader = new TableReader.XMLReader(@"c:\", "Foglio1");
            excelReader.Print();
            Console.ReadKey();

            excelReader = new TableReader.XMLReader(CiaoFilePath, "Foglio1");
            excelReader.Print();
            Console.ReadKey();

            var CSVReader = new TableReader.CSVReader(CSVFilePath);
            CSVReader.Print();
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
        private static string CSVFilePath
        {
            get
            {
                return Path.Combine(TestFilesDirectory, "Test.csv");
            }
        }
    }

}

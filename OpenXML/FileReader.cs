using System.IO;
using System.Data;
using System;
using System.Collections.Generic;

namespace TableReader
{
    public abstract class FileReader
    {
        protected string FilePath;
        protected bool FileIsFound;
        protected bool FileIsSupported;
        public FileReader(string path)
        {
            FilePath = path;
        }

        /// <summary>
        /// Get if file exists
        /// </summary>
        /// <returns>
        /// True if file exists
        /// False if file does not exist
        /// </returns>
        protected bool FileExists()
        {
           return File.Exists(FilePath);
        }

        /// <summary>
        /// Get file extension
        /// </summary>
        /// <returns>
        /// File extension, example: .csv, .xlsm, .xlsx
        /// </returns>
        protected string GetFileExtension()
        {
            return Path.GetExtension(FilePath);
        }

        protected string CopyFileToAppData()
        {
            // The folder for the roaming current user 
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            // Combine the base folder with your specific folder....
            string specificFolder = Path.Combine(appDataFolder, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
            // CreateDirectory will check if folder exists and, if not, create it.
            // If folder exists then CreateDirectory will do nothing.
            Directory.CreateDirectory(specificFolder);
            string outputPath = Path.Combine(specificFolder, Path.GetFileName(FilePath));
            using (var inputFile = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var outputFile = new FileStream(outputPath, FileMode.Create))
                {
                    var buffer = new byte[0x10000];
                    int bytes;
                    while ((bytes = inputFile.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        outputFile.Write(buffer, 0, bytes);
                    }
                }
            }
            return outputPath;
        }

        protected abstract bool IsFileSupported();
        protected abstract DataTable ReadFile();
        public abstract ReadResult Read();
        protected abstract List<string> SupportedFiles();
        public  void Print()
        {
            ReadResult readResult = Read();
            if (readResult.IsFileFound)
            {
                if (readResult.IsFileTypeSupported)
                {
                    var table = readResult.Table;
                    if (table != null)
                    {
                        table.Print();
                    }
                    else
                    {
                        Console.WriteLine("Data table is null");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid fle type");
                }
            }
            else
            {
                Console.WriteLine("Invalid fle path");
            }
        }
    }
}

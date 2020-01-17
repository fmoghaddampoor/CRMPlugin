using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TableReader
{
    public class CSVReader : FileReader
    {
        public CSVReader(string path) : base(path)
        {
            FileIsFound = FileExists();
            FileIsSupported = IsFileSupported();
        }

        public override ReadResult Read()
        {
            ReadResult readResult = new ReadResult();
            if (!FileIsFound || !FileIsSupported) return new ReadResult() { Table = null, IsFileFound = FileIsFound, IsFileTypeSupported = FileIsSupported };
            DataTable dt = ReadFile();
            readResult = new ReadResult() { Table = dt, IsFileFound = true, IsFileTypeSupported = true };
            return readResult;
        }

        protected override bool IsFileSupported()
        {
            if (!FileIsFound) return false;
            var fileExtension = GetFileExtension();
            return IsFileExtensionSupported(fileExtension);
        }

        protected override DataTable ReadFile()
        {
            DataTable dt = null;
            string[] CSVLines = ReadFileLines();
            if (CSVLines.Length > 0)
            {
                dt = new DataTable();
                char delimiter = GetFileDelimiter(CSVLines[0]);
                string[] headers = CSVLines[0].Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                for (int locIndexLine = 1; locIndexLine < CSVLines.Length; locIndexLine++)
                {
                    string[] cells = CSVLines[locIndexLine].Split(new char[] { delimiter });
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = cells[i];
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        private string[] ReadFileLines()
        {
            string FileAppDataPath = CopyFileToAppData();
            List<string> lstLines = new List<string>();
            using (System.IO.FileStream fileStream = new System.IO.FileStream(FileAppDataPath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read))
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader(fileStream, Encoding.UTF8))
                {
                    string line = String.Empty;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lstLines.Add(line);
                    }
                }
            }
            return lstLines.ToArray();
        }

        private char GetFileDelimiter(string input)
        {
            var delimiters = GetDelimiters();
            Dictionary<char, int> dicCharCount = delimiters.ToDictionary(key => key, value => 0);
            foreach (char delimiter in delimiters)
            {
                dicCharCount[delimiter] = input.Count(t => t == delimiter);
            }
            return dicCharCount.OrderByDescending(x => x.Value).First().Key;
        }

        private List<char> GetDelimiters()
        {
            return new List<char> { ' ', ';', '-', ',', '|' };
        }

        private bool IsFileExtensionSupported(string extension)
        {
            return SupportedFiles().Contains(extension);
        }

        protected override List<string> SupportedFiles()
        {
            return new List<string>() { ".csv" };
        }
    }
}

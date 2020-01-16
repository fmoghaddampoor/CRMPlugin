using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace TableReader
{
    public class OpenXMLReader
    {
        private readonly string _filePath;
        private readonly string _sheetName;
        private bool _isFileFound;
        private bool _isFileSupported;
        public OpenXMLReader(string path, string sheetName)
        {
            _filePath = path;
            _sheetName = sheetName;
            _isFileFound = IsFileFound(path);
            _isFileSupported = IsFileSupported(path);

        }
        public ReadResult Read()
        {
            ReadResult readResult = new ReadResult();
            if (!_isFileFound || !_isFileSupported) return new ReadResult() { Table = null, IsFileFound = _isFileFound, IsFileTypeSupported = _isFileSupported };
            DataTable dt = ReadExcelFile();
            readResult = new ReadResult() { Table = dt, IsFileFound = true, IsFileTypeSupported = true };
            return readResult;
        }

        private string CopyFileToAppData()
        {
            // The folder for the roaming current user 
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            // Combine the base folder with your specific folder....
            string specificFolder = Path.Combine(appDataFolder, "OpenXML");
            // CreateDirectory will check if folder exists and, if not, create it.
            // If folder exists then CreateDirectory will do nothing.
            Directory.CreateDirectory(specificFolder);
            string outputPath = Path.Combine(specificFolder, Path.GetFileName(_filePath));
            using (var inputFile = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
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
        private DataTable ReadExcelFile()
        {
            string outputPath = CopyFileToAppData();
            DataTable dataTable = new DataTable();
            using (DocumentFormat.OpenXml.Packaging.SpreadsheetDocument spreadsheetDocument = DocumentFormat.OpenXml.Packaging.SpreadsheetDocument.Open(outputPath, false))
            {
                DocumentFormat.OpenXml.Packaging.WorksheetPart worksheetPart = GetWorksheetPartByName(spreadsheetDocument, _sheetName);
                DocumentFormat.OpenXml.Spreadsheet.SheetData sheetData = worksheetPart.Worksheet.Elements<DocumentFormat.OpenXml.Spreadsheet.SheetData>().First();
                IEnumerable<Row> rows = sheetData.Descendants<Row>();
                foreach (Cell cell in rows.ElementAt(0))
                {
                    dataTable.Columns.Add(GetCellValue(spreadsheetDocument, cell));
                }
                for (int i=1;i< rows.Count()-1;i++)
                {
                    DataRow tempRow = dataTable.NewRow();
                    for (int j = 0; j < rows.ElementAt(i).Descendants<Cell>().Count(); j++)
                    {
                        tempRow[j] = GetCellValue(spreadsheetDocument, rows.ElementAt(i).Descendants<Cell>().ElementAt(j));
                    }
                    dataTable.Rows.Add(tempRow);
                }
            }
            return dataTable;
        }
        private bool IsFileFound(string path)
        {
            return File.Exists(path);
        }
        private bool IsFileSupported(string path)
        {
            if (!_isFileFound) return false;
            var fileExtension = Path.GetExtension(_filePath);
            return IsFileExtensionSupported(fileExtension);
        }

        private bool IsFileExtensionSupported(string extension)
        {
            List<string> lstSupportedFileTypes = new List<string>() { ".xlsx", ".xlsm" };
            return lstSupportedFileTypes.Contains(extension);
        }

        public class ReadResult
        {
            public DataTable Table { set; get; }
            public bool IsFileFound { set; get; }
            public bool IsFileTypeSupported { set; get; }
            public ReadResult() 
            {
                Table = new DataTable();
                IsFileFound = true;
                IsFileTypeSupported = true;
            }
        }

        public void Print()
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

        private string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;
            string value = (cell.CellValue == null) ? "" : cell.CellValue.InnerXml;
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
            }
            else
            {
                return value;
            }
        }

        private WorksheetPart GetWorksheetPartByName(SpreadsheetDocument document, string sheetName)
        {
            IEnumerable<Sheet> sheets =
               document.WorkbookPart.Workbook.GetFirstChild<Sheets>().
               Elements<Sheet>().Where(s => s.Name == sheetName);

            if (sheets?.Count() == 0)
            {
                // The specified worksheet does not exist.

                return null;
            }
            string relationshipId = sheets?.First().Id.Value;
            WorksheetPart worksheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(relationshipId);
            return worksheetPart;
        }


    }

}

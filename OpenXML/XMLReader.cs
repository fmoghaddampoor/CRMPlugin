using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace TableReader
{
    public class XMLReader: FileReader
    {
        private readonly string _sheetName;
        public XMLReader(string path, string sheetName): base(path)
        {
            _sheetName = sheetName;
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

        protected override DataTable ReadFile()
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
                for (int i=1; i< rows.Count()-1; i++)
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
        protected override bool IsFileSupported()
        {
            if (!FileIsFound) return false;
            var fileExtension = GetFileExtension();
            return IsFileExtensionSupported(fileExtension);
        }

        private bool IsFileExtensionSupported(string extension)
        {
            return SupportedFiles().Contains(extension);
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

        protected override List<string> SupportedFiles()
        {
            return new List<string>() { ".xlsx", ".xlsm" };
        }
    }
}

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
        public ReadResult Read(string path, string sheetName)
        {
            ReadResult readResult = new ReadResult();
            if (!File.Exists(path))
            {
                readResult = new ReadResult() { Table = null, TableFileFound = false };
            }
            else
            {
                var fileExtension = Path.GetExtension(path);
                List<string> lstSupportedFileTypes = new List<string>() { ".xlsx", ".xlsm" };
                if (!lstSupportedFileTypes.Contains(fileExtension))
                {
                    readResult = new ReadResult() { Table = null, TableFileFound = true, IsFileTypeSupported = false };
                }
                else
                {
                    readResult = new ReadResult() { Table = new DataTable(), TableFileFound = true, IsFileTypeSupported = true };
                    // Open the document for editing.
                    using (DocumentFormat.OpenXml.Packaging.SpreadsheetDocument spreadsheetDocument = DocumentFormat.OpenXml.Packaging.SpreadsheetDocument.Open(path, false))
                    {
                        DocumentFormat.OpenXml.Packaging.WorksheetPart worksheetPart = GetWorksheetPartByName(spreadsheetDocument, sheetName);
                        DocumentFormat.OpenXml.Spreadsheet.SheetData sheetData = worksheetPart.Worksheet.Elements<DocumentFormat.OpenXml.Spreadsheet.SheetData>().First();
                        IEnumerable<Row> rows = sheetData.Descendants<Row>();
                        foreach (Cell cell in rows.ElementAt(0))
                        {
                            readResult.Table.Columns.Add(GetCellValue(spreadsheetDocument, cell));
                        }
                        foreach (Row row in rows) //this will also include your header row...
                        {
                            DataRow tempRow = readResult.Table.NewRow();
                            for (int i = 0; i < row.Descendants<Cell>().Count(); i++)
                            {
                                tempRow[i] = GetCellValue(spreadsheetDocument, row.Descendants<Cell>().ElementAt(i));
                            }
                            readResult.Table.Rows.Add(tempRow);
                        }
                    }
                }

            }
            return readResult;
        }

        public class ReadResult
        {
            public DataTable Table { set; get; }
            public bool TableFileFound { set; get; }
            public bool IsFileTypeSupported { set; get; }
            public ReadResult() 
            {
                Table = new DataTable();
                TableFileFound = true;
                IsFileTypeSupported = true;
            }
        }

        public void Print(string path, string sheetName)
        {
            OpenXMLReader excelReader = new OpenXMLReader();
            ReadResult readResult = excelReader.Read(path, sheetName);
            if (readResult.TableFileFound)
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

using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;

namespace TableReader.UnitTests
{
    [TestFixture]
    class OpenXMLReaderXLSXTests
    {

        private TableReader.XMLReader _excelReader = null;
        [OneTimeSetUp]
        public void ClassInit()
        {
            _excelReader = new TableReader.XMLReader(XLSXFilePath, Commons.Sheet1);
        }

        [Test]
        public void Read_WhenFileIsXlsx_TableIsNotNull()
        {
            var readResult = _excelReader.Read();
            Assert.That(readResult.Table, Is.Not.Null);
        }

        [Test]
        public void Read_WhenFileIsXlsx_IsFileTypeSupportedIsTrue()
        {
            var readResult = _excelReader.Read();
            Assert.That(readResult.IsFileTypeSupported, Is.True);
        }

        [Test]
        public void Read_WhenFileIsXlsx_TableFileFoundIsTrue()
        {
            var readResult = _excelReader.Read();
            Assert.That(readResult.IsFileFound, Is.True);
        }

        private static string XLSXFilePath
        {
            get
            {
                return Path.Combine(Commons.GetTestFilesDirectory(), "Test.xlsx");
            }
        }
    }
}

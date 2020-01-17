using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;

namespace TableReader.UnitTests
{
    [TestFixture]
    class OpenXMLReaderXLSMTests
    {
        private TableReader.XMLReader _excelReader = null;

        [OneTimeSetUp]
        public void ClassInit()
        {
            _excelReader = new TableReader.XMLReader(XLSMFilePath, Commons.Sheet1);
        }

        [Test]
        public void Read_WhenFileIsXlsm_TableIsNotNull()
        {
            var readResult = _excelReader.Read();
            Assert.That(readResult.Table, Is.Not.Null);
        }

        [Test]
        public void Read_WhenFileIsXlsm_IsFileTypeSupportedIsTrue()
        {
            var readResult = _excelReader.Read();
            Assert.That(readResult.IsFileTypeSupported, Is.True);
        }

        [Test]
        public void Read_WhenFileIsXlsm_TableFileFoundIsTrue()
        {
            var readResult = _excelReader.Read();
            Assert.That(readResult.IsFileFound, Is.True);
        }

        private static string XLSMFilePath
        {
            get
            {
                return Path.Combine(Commons.GetTestFilesDirectory(), "Test.xlsm");
            }
        }
    }
}

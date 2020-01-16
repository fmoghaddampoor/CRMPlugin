using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;

namespace TableReader.UnitTests
{
    [TestFixture]
    class OpenXMLReaderCiaoTests
    {
        private TableReader.OpenXMLReader _excelReader = null;

        [OneTimeSetUp]
        public void ClassInit()
        {
            _excelReader = new TableReader.OpenXMLReader(CiaoFilePath, Commons.Sheet1);
        }

        [Test]
        public void Read_WhenFileIsCiao_TableIsNull()
        {
            var readResult = _excelReader.Read();
            Assert.That(readResult.Table, Is.Null);
        }

        [Test]
        public void Read_WhenFileIsCiao_IsFileTypeSupportedIsFalse()
        {
            var readResult = _excelReader.Read();
            Assert.That(readResult.IsFileTypeSupported, Is.False);
        }

        [Test]
        public void Read_WhenFileIsCiao_TableFileFoundIsTrue()
        {
            var readResult = _excelReader.Read();
            Assert.That(readResult.IsFileFound, Is.True);
        }
        private static string CiaoFilePath
        {
            get
            {
                return Path.Combine(Commons.GetTestFilesDirectory(), "Test.ciao");
            }
        }
    }
}

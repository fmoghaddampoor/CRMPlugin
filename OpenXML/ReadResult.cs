using System.Data;

namespace TableReader
{
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
}

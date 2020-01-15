using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser.Mapping;

namespace CSVDataUploader
{
    public class CsvPersonMapping : CsvMapping<Account>
    {
        public CsvPersonMapping(): base()
        {
            MapProperty(0, x => x.Name);
            MapProperty(1, x => x.PrimaryContactId);
            MapProperty(2, x => x.Telephone1);
        }
    }
}

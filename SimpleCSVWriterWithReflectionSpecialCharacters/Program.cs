using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVManager
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Person(1, "\"Hello\", world!", "İĞÜÇÖıüşöç");
            Console.WriteLine(p.ToCSV());
            Console.ReadLine();
        }
    }
}

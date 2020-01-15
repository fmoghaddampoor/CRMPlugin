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
            var p = new Person(1, "murat", "aykanat",
                            new Address("city1", "country1"));
            Console.WriteLine(p.ToCsv());
            Console.ReadLine();
        }
    }
}

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
            var person = new Person(1, "murat", "aykanat");
            Console.WriteLine(person.ToCSV());
            Console.ReadLine();
        }
    }
}

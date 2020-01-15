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
            var p = new Person(1, "murat", "aykanat");
            Console.WriteLine("Ignore by property name:");
            Console.WriteLine("Ignoring Id property: "
            + p.ToCSV(new[] { "Id" }, true));
            Console.WriteLine("Ignoring Name property: "
            + p.ToCSV(new[] { "Name" }, true));
            Console.WriteLine("Ignoring Lastname property: "
            + p.ToCSV(new[] { "Lastname" }, true));
            Console.WriteLine("Ignore by property index:");
            Console.WriteLine("Ignoring 0->Id and 2->Lastname: "
            + p.ToCSV(new[] { 0, 2 }, true));
            Console.WriteLine("Ignoring everything but Id: "
            + p.ToCSV(new[] { "Id" }, false));
            Console.ReadLine();
        }
    }
}

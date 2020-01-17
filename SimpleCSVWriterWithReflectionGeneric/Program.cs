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
            var people = new List<Person>
            {
                new Person(1, "Farzad", "Moghaddampoor", new Address("Milano","Italy")),
                new Person(2, "Andrea", "Porelli", new Address("city2","country2"))
            };

            var csvWriter = new CSVWriter<Person>();
            csvWriter.Write(people, "example.csv");

            var csvReader = new CSVReader<Person>();
            var csvPeople = csvReader.Read("example.csv", true);

            foreach (var person in csvPeople)
            {
                Console.WriteLine(person);
            }

            Console.ReadLine();
        }
    }
}

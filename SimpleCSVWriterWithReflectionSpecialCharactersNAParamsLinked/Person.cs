using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVManager
{
    public class Person : CSVableBase
    {
        public Person(int id, string name, string lastname, Address address)
        {
            Id = id;
            Name = name;
            Lastname = lastname;
            Address = address;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public Address Address { get; set; }
    }
}

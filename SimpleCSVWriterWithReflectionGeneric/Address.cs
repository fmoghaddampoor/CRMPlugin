using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVManager
{
    public class Address : CSVableBase
    {
        public Address()
        {

        }

        public Address(string city, string country)
        {
            City = city;
            Country = country;
        }
        public string City { get; set; }
        public string Country { get; set; }
        public override string ToString()
        {
            return " " + City + " / " + Country;
        }
    }
}

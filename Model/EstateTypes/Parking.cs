using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Modern_Real_Estate.Model.EstateTypes
{
    public class Parking : Estate
    {
        public static string SubType => "Parking";

        public Parking(string streetName, int zipCode, string city, string country, decimal price)
        {
            StreetName = streetName;
            ZipCode = zipCode;
            City = city;
            Country = country;
            Price = price;
        }
    }
}

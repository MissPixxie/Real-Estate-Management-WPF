using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modern_Real_Estate.Model.EstateTypes
{
    public class Storage : Estate
    {
        public static string SubType => "Storage";

        public Storage(string streetName, int zipCode, string city, string country, int sqrM, decimal price)
        {
            StreetName = streetName;
            ZipCode = zipCode;
            City = city;
            Country = country;
            SqrM = sqrM;
            Price = price;
        }
    }
}

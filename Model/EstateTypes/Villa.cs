using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modern_Real_Estate.Model.EstateTypes
{
    public class Villa : House
    {
        public static string SubType => "Villa";
        public Villa() { }
        public Villa(string streetName, int zipCode, string city, string country, int area, int rooms, int sqrM, decimal price)
        {
            StreetName = streetName;
            ZipCode = zipCode;
            City = city;
            Country = country;
            Area = area;
            Rooms = rooms;
            SqrM = sqrM;
            Price = price;
        }

    }
}

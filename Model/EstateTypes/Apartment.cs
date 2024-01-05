using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Modern_Real_Estate.Model.EstateTypes
{
    public class Apartment : Residential
    {
        public static string SubType => "Apartment";

        public Apartment(string streetName, int zipCode, string city, string country, int rooms, int sqrM, decimal price)
        {
            StreetName = streetName;
            ZipCode = zipCode;
            City = city;
            Country = country;
            Rooms = rooms;
            SqrM = sqrM;
            Area = sqrM;
            Price = CalculatePrice(price);
        }

        public override decimal CalculatePrice(decimal price)
        {
            decimal vat = price * 0.12m;

            return Math.Floor(price + vat);
        }
    }
}

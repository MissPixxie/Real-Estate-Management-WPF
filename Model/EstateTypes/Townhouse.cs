using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modern_Real_Estate.Model.EstateTypes
{
    public class Townhouse : Residential
    {
        private string _subType;
        public string SubType
        {
            get { return "Townhouse"; }
        }
        public Townhouse() { }
        public Townhouse(string streetName, int zipCode, string city, string country, int area, int rooms, int sqrM, double price)
        {
            StreetName = streetName;
            ZipCode = zipCode;
            City = city;
            Country = country;
            Area = area;
            Rooms = rooms;
            SqrM = sqrM;
            Price = CalculatePrice(price);
        }

        public override double CalculatePrice(double price)
        {
            double vat = price * 0.12;

            return price + vat;
        }
    }
}

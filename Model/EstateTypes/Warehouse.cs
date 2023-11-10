using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modern_Real_Estate.Model.EstateTypes
{
    public class Warehouse : Commercial
    {
        private string _subType;
        public string SubType
        {
            get { return "Warehouse"; }
        }

        public Warehouse() { }
        public Warehouse(string streetName, int zipCode, string city, string country, int area, double price)
        {
            StreetName = streetName;
            ZipCode = zipCode;
            City = city;
            Country = country;
            Area = area;
            Price = CalculatePrice(price);
        }



        public override double CalculatePrice(double price)
        {
            double vat = price * 0.25;

            return price + vat;
        }
    }
}

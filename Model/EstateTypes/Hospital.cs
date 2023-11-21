using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modern_Real_Estate.Model.EstateTypes
{
    public class Hospital : Institutional
    {
        private string _subType;
        public string SubType
        {
            get { return "Hospital"; }
        }
        public Hospital() { }
        public Hospital(string streetName, int zipCode, string city, string country, int area, decimal price)
        {
            StreetName = streetName;
            ZipCode = zipCode;
            City = city;
            Country = country;
            Area = area;
            Price = CalculatePrice(price);
        }


        public override decimal CalculatePrice(decimal price)
        {
            decimal vat = price * 0.25m;

            return Math.Floor(price + vat);
        }
    }
}

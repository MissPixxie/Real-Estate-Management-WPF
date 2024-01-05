﻿using System;
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
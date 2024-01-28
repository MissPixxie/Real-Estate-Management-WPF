using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modern_Real_Estate.Model.EstateTypes
{
    public class Rentals : Apartment
    {
        private string _type;

        public Rentals(string streetName, int zipCode, string city, string country, int rooms, int sqrM, decimal price) : base(streetName, zipCode, city, country, rooms, sqrM, price)
        {
        }

        public string Type
        {
            get { return "Rentals"; }
        }

  
     }
}

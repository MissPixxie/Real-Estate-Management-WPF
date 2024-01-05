using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modern_Real_Estate.Model;

namespace Modern_Real_Estate.Model
{
    interface IEstate
    {
        int Id { get; }
        string StreetName { get; set; }
        int ZipCode { get; set; }
        string City { get; set; }
        string Country { get; set; }
        int SqrM { get; set; }
        bool IsRental { get; set; }
        decimal Price { get; set; }
        public byte[]? Image { get; set; }

    }

}

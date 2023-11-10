using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using Modern_Real_Estate.Model;
using Modern_Real_Estate.Model.EstateTypes;
using Modern_Real_Estate.Utilities;

namespace Modern_Real_Estate.Model
{
    [Serializable]
    [XmlInclude(typeof(Apartment))]
    [XmlInclude(typeof(Hospital))]
    [XmlInclude(typeof(School))]
    [XmlInclude(typeof(Shop))]
    [XmlInclude(typeof(Townhouse))]
    [XmlInclude(typeof(University))]
    [XmlInclude(typeof(Villa))]
    [XmlInclude(typeof(Warehouse))]
    public abstract class Estate : ObservableObject, IEstate, IComparable<Estate>
    {

        private static int _nextId = 0;
        private int _id = 0;

        public int Id
        {
            get
            {
                return _id;
            }
        }

        public abstract int this[int index] { get; set; }

        public Estate()
        {
            _nextId++;
            _id = _nextId;
        }


        private string _streetName;
        public string StreetName
        {
            get { return _streetName; }
            set
            {
                _streetName = value;
                OnPropertyChanged(nameof(StreetName));
            }
        }


        private int _zipCode;
        public int ZipCode
        {
            get { return _zipCode; }
            set
            {
                _zipCode = value;
                OnPropertyChanged(nameof(ZipCode));
            }
        }


        private string _city;
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }


        private string _country;
        public string Country
        {
            get { return _country; }
            set
            {
                _country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        private int _area;
        public int Area
        {
            get { return _area; }
            set
            {
                _area = value;
                OnPropertyChanged(nameof(Area));
            }
        }

        private double _price;
        public double Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        private byte[] _image;
        public byte[]? Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        public abstract double CalculatePrice(double price);

        public override string ToString()
        {
            return $"Id: { Id }, Streetname: { StreetName }, Zipcode: { ZipCode }, City: { City }, Country: { Country }, Area: { Area }, Price: { Price } ";
        }

        public int CompareTo(Estate? other)
        {
            if (other == null) 
                return 1;
            else
            {
                return StreetName.CompareTo(other.StreetName);
            }
        }
    }

}

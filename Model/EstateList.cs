using Modern_Real_Estate.Model.EstateTypes;
using Modern_Real_Estate.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.DirectoryServices;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Modern_Real_Estate.Model
{
    public class EstateList : ObservableObject
    {

       public ObservableCollection<Estate> Estates => SharedCollection;

        private static ObservableCollection<Estate> _sharedCollection = new ObservableCollection<Estate>();

        public static ObservableCollection<Estate> SharedCollection
        {
            get { return _sharedCollection; }
            set
            {
                _sharedCollection = value;
            }
        }


        public EstateList()
        {
            SharedCollection.CollectionChanged += SharedCollection_Changed;
        }

        public void SharedCollection_Changed(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Estates));
        }


        public static bool Create(Estate estate)
        {
            _sharedCollection.Add(estate);

            return true;
        }


        public static void Delete(Estate estate)
        {
            _sharedCollection.Remove(estate);
        }


        public static bool Update(Estate estate, string? streetName = null, int? zipCode = null, string? city = null, string? country = null, int? area = null, byte[]? selectedImage = null, int? rooms = null, int? sqrM = null, int? price = null)
        {
            int IdToFind = estate.Id;

            Estate foundEstate = _sharedCollection.FirstOrDefault( e => e.Id == IdToFind );

            if (foundEstate != null && foundEstate is Residential residential)
            {
                if (streetName != null)
                {
                    foundEstate.StreetName = streetName;
                }

                if (zipCode != null)
                {
                    foundEstate.ZipCode = zipCode.Value;
                }

                if (city != null)
                {
                    foundEstate.City = city;
                }

                if (country != null)
                {
                    foundEstate.Country = country;
                }
                if (foundEstate is House house && area != null)
                {
                    house.Area = area.Value;
                }
                if (selectedImage != null)
                {
                    foundEstate.Image = selectedImage;
                }
                if (rooms != null)
                {
                    residential.Rooms = rooms.Value;
                }

                if (sqrM != null)
                {
                    residential.SqrM = sqrM.Value;
                }
                if (price != null)
                {
                    foundEstate.Price = price.Value;
                }
            }
            return true;

        }
        public static bool Update(Estate estate, string streetName, int zipCode, string city, string country, int? area = null, int? price = null)
        {
            int IdToFind = estate.Id;

            Estate foundEstate = _sharedCollection.FirstOrDefault(e => e.Id == IdToFind);

            if (foundEstate != null)
            {
                if (streetName != null)
                {
                    foundEstate.StreetName = streetName;
                }

                if (zipCode != null)
                {
                    foundEstate.ZipCode = zipCode;
                }

                if (city != null)
                {
                    foundEstate.City = city;
                }

                if (country != null)
                {
                    foundEstate.Country = country;
                }
                if (foundEstate is House house && area != null)
                {
                    house.Area = area.Value;
                }
                if (price != null)
                {
                    foundEstate.Price = price.Value;
                }
            }
            return true;

        }

    }
}

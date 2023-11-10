using Modern_Real_Estate.Model;
using Modern_Real_Estate.Model.EstateTypes;
using Modern_Real_Estate.Utilities;
using Modern_Real_Estate.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Modern_Real_Estate.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        public EstateManager estateManager { get; set; }
        private string fileName = Environment.CurrentDirectory + "\\estateList.dat";  //file at Application directory
        private string xmlFileName = Environment.CurrentDirectory + "\\estateList.xml";  //(file for testi
        //private string filePath = Environment.CurrentDirectory + @"\estateList.xml";

        // public List<string> StringsList { get; set; }

        //public string EstateList { get; set; }


        //public EstateList Estates { get; set; }
        //public ObservableCollection<Apartment> Apartments { get; set; }
        //public ObservableCollection<Hospital> Hospitals { get; set; }
        //public ObservableCollection<SchoolViewModel> Schools { get; set; }
        //public ObservableCollection<Shop> Shops { get; set; }
        //public ObservableCollection<TownhouseViewModel> Townhouses { get; set; }
        //public ObservableCollection<UniversityViewModel> Universitys { get; set; }
        //public ObservableCollection<VillaViewModel> Villas { get; set; }
        //public ObservableCollection<WarehouseViewModel> Warehouses { get; set; }
        //public ObservableCollection<string> Countries { get; set; }


        //public string ApartmentList { get; set; }
        //public string HospitalList { get; set; }
        //public string SchoolList { get; set; }
        //public string ShopList { get; set; }
        //public string TownhouseList { get; set; }
        //public string UniversityList { get; set; }
        //public string VillaList { get; set; }
        //public string WarehouseList { get; set; }

        public RelayCommand AddCommand => new RelayCommand(execute => AddEstate());
        public RelayCommand DeleteCommand => new RelayCommand(execute => DeleteEstate());
        public RelayCommand SaveCommand => new RelayCommand(execute => SaveEstate());
        public RelayCommand ClearCommand => new RelayCommand(execute => ClearEstate());

      


        public HomeViewModel()
        {
            //Estate apartment = new Apartment("hej", 2222, "stad", "land", 3, 73, 5000);

            //estateManager.Add(apartment);
        }

        private Estate _selectedEstate;
        public Estate SelectedEstate
        {
            get { return _selectedEstate; }
            set
            {
                if (_selectedEstate != value)
                {
                    _selectedEstate = value;
                    OnPropertyChanged(nameof(SelectedEstate));
                }
            }
        }

        public void AddEstate()
        {

            Estate apartment = new Apartment("hej", 2222, "stad", "land", 3, 73, 5000);

            estateManager.Add(apartment);

        }

        public void DeleteEstate()
        {
            if (SelectedEstate != null)
            {
                estateManager.DeleteAt(SelectedEstate.Id);
            }
        }

        public void SaveEstate()
        {
            estateManager.ChangeAt(SelectedEstate, 0);

        }


        public void ClearEstate()
        {
            estateManager.DeleteAll();
        }

        // switch case apartment...... or residential

        // switch the usercontrol

    }
}

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
        public EstateManager EstateManager { get; set; }

        public ObservableCollection<string>? Countries { get; set; }

        //private string fileName = Environment.CurrentDirectory + "\\estateList.dat";
        //private string xmlFileName = Environment.CurrentDirectory + "\\estateList.xml";

        public int? TextBoxValueId { get; set; }
        public string? TextBoxValueStreetName { get; set; }
        public int? TextBoxValueZipCode { get; set; }
        public string? TextBoxValueCity { get; set; }
        public string? TextBoxValueCountry { get; set; }
        public int? TextBoxValueArea { get; set; }
        public int? TextBoxValueRooms { get; set; } 
        public int? TextBoxValueSqrM { get; set; } 
        public string? TextBoxValueType { get; }
        public decimal? TextBoxValuePrice { get; set; }

        public HomeViewModel()
        {
            EstateManager = EstateManager.GetInstance();
        }

        private string? _selectedCountry;
        public string? SelectedCountry
        {
            get { return _selectedCountry; }
            set
            {
                _selectedCountry = value;
                OnPropertyChanged();
            }
        }

        public void SetCountry()
        {
            foreach (string countryName in Enum.GetNames(typeof(Countries)))
            {
                Countries.Add(countryName);
            }
        }

        private void UpdateTextBoxValues()
        {
            TextBoxValueId = _selectedEstate.Id;
            TextBoxValueStreetName = _selectedEstate?.StreetName ?? "";
            TextBoxValueZipCode = _selectedEstate?.ZipCode ?? 0;
            TextBoxValueCity = _selectedEstate?.City ?? "";
            TextBoxValueCountry = _selectedEstate?.Country ?? "";
            TextBoxValuePrice = _selectedEstate?.Price ?? 0;
            if (_selectedEstate is House townhouse)
            {
                TextBoxValueArea = townhouse.SqrM;
                TextBoxValueRooms = townhouse.Rooms;
                TextBoxValueSqrM = townhouse.SqrM;
            }
            else
            {
                TextBoxValueRooms = 0;
                TextBoxValueSqrM = 0;
            }

            OnPropertyChanged(nameof(TextBoxValueId));
            OnPropertyChanged(nameof(TextBoxValueStreetName));
            OnPropertyChanged(nameof(TextBoxValueZipCode));
            OnPropertyChanged(nameof(TextBoxValueCity));
            OnPropertyChanged(nameof(TextBoxValueCountry));
            OnPropertyChanged(nameof(TextBoxValueArea));
            OnPropertyChanged(nameof(TextBoxValueRooms));
            OnPropertyChanged(nameof(TextBoxValueSqrM));
            OnPropertyChanged(nameof(TextBoxValuePrice));
        }


        private Estate? _selectedEstate = null;
        public Estate? SelectedEstate
        {
            get { return _selectedEstate; }
            set
            {
                _selectedEstate = value;
                OnPropertyChanged(nameof(SelectedEstate));
                UpdateTextBoxValues();
            }
        }

        public void Reset()
        {
            SelectedEstate = null;
        }


        private Estate? _selectedTextBoxValue;
        public Estate? SelectedTextBoxValue
        {
            get { return _selectedTextBoxValue; }
            set
            {
                if (_selectedTextBoxValue != value)
                {
                    _selectedTextBoxValue = value;
                    OnPropertyChanged(nameof(SelectedTextBoxValue));
                    UpdateTextBoxValues();
                }
            }
        }

        // switch case apartment...... or residential

        // switch the usercontrol

    }
}

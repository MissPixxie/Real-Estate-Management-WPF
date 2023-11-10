using Microsoft.Win32;
using Modern_Real_Estate.Model;
using Modern_Real_Estate.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Modern_Real_Estate.Model.EstateTypes;

namespace Modern_Real_Estate.ViewModel
{
    public class SchoolViewModel : ViewModelBase
    {
        public EstateManager estateManager { get; set; }
        public ObservableCollection<string> Countries { get; set; }
        public int TextBoxValueId { get; set; }
        public string TextBoxValueStreetName { get; set; }
        public int TextBoxValueZipCode { get; set; }
        public string TextBoxValueCity { get; set; }
        public string TextBoxValueCountry { get; set; }
        public int TextBoxValueArea { get; set; }
        public string TextBoxValueType { get; }
        public double TextBoxValuePrice { get; set; }

        public RelayCommand AddCommand => new RelayCommand(execute => AddEstate());
        public RelayCommand DeleteCommand => new RelayCommand(execute => DeleteEstate(), canExecute => SelectedEstate != null);
        public RelayCommand SaveCommand => new RelayCommand(execute => UpdateEstate(), canExecute => CanSave());
        public RelayCommand ImagePickerCommand => new RelayCommand(execute => OpenImagePicker());


        public SchoolViewModel()
        {
            estateManager = EstateManager.GetInstance();
            Countries = new ObservableCollection<string>();


            SetCountry();
        }

        private string _selectedCountry;
        public string SelectedCountry
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
            TextBoxValueStreetName = _selectedEstate?.StreetName ?? "";
            TextBoxValueZipCode = _selectedEstate?.ZipCode ?? 0;
            TextBoxValueCity = _selectedEstate?.City ?? "";
            TextBoxValueCountry = _selectedEstate?.Country ?? "";
            TextBoxValueArea = _selectedEstate?.Area ?? 0;
            TextBoxValuePrice = _selectedEstate?.Price ?? 0;


            OnPropertyChanged(nameof(TextBoxValueStreetName));
            OnPropertyChanged(nameof(TextBoxValueZipCode));
            OnPropertyChanged(nameof(TextBoxValueCity));
            OnPropertyChanged(nameof(TextBoxValueCountry));
            OnPropertyChanged(nameof(TextBoxValueArea));
            OnPropertyChanged(nameof(TextBoxValuePrice));
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
                    UpdateTextBoxValues();
                }
            }
        }

        private Estate _selectedTextBoxValue;
        public Estate SelectedTextBoxValue
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


        public void AddEstate()
        {
            Estate newEstate = new School(
                TextBoxValueStreetName,
                TextBoxValueZipCode,
                TextBoxValueCity,
                TextBoxValueCountry,
                TextBoxValueArea,
                TextBoxValuePrice
            );

            EstateList.Create(newEstate);

            UpdateTextBoxValues();

        }

        public void DeleteEstate()
        {
            EstateList.Delete(SelectedEstate);
        }

        public void UpdateEstate()
        {
            if (SelectedEstate != null)
            {
                var isUpdated = EstateList.Update(SelectedEstate, TextBoxValueStreetName, TextBoxValueZipCode, TextBoxValueCity, TextBoxValueCountry, TextBoxValueArea, TextBoxValuePrice);
                if (isUpdated)
                {
                    OnPropertyChanged(nameof(EstateManager));
                }
            }
        }

        private bool CanSave()
        {
            return true;
        }

        private ImageSource _selectedImage;
        public ImageSource SelectedImage
        {
            get { return _selectedImage; }
            set
            {
                _selectedImage = value;
                OnPropertyChanged(nameof(SelectedImage));
            }
        }

        public byte[]? imageBytes { get; private set; }

        private void OpenImagePicker()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Bildfiler (*.jpg; *.png)|*.jpg;*.png|Alla filer (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedImagePath = openFileDialog.FileName;
                SelectedImage = new BitmapImage(new Uri(selectedImagePath));
            }
        }
    }
}

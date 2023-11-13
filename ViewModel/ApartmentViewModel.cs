using Microsoft.Win32;
using Modern_Real_Estate.Model;
using Modern_Real_Estate.Model.EstateTypes;
using Modern_Real_Estate.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Modern_Real_Estate.ViewModel
{
    public class ApartmentViewModel : ViewModelBase
    {

        public EstateManager estateManager { get; set; }
        public ObservableCollection<string> Countries { get; set; }
        //public int SelectedIndex { get; set; }
      
        public int TextBoxValueId { get; set; }
        public string TextBoxValueStreetName { get; set; }
        public int TextBoxValueZipCode { get; set; }
        public string TextBoxValueCity { get; set; }
        public string TextBoxValueCountry { get; set; }
        public int TextBoxValueArea { get; set; }
        public int TextBoxValueRooms { get; set; }
        public int TextBoxValueSqrM { get; set; }
        public string TextBoxValueType { get; }
        public double TextBoxValuePrice { get; set; }
        public double TextBoxValueVATPrice { get; set; }


        public RelayCommand AddCommand => new RelayCommand(execute => AddEstate());
        public RelayCommand DeleteCommand => new RelayCommand(execute => DeleteEstate(), canExecute => SelectedEstate != null);
        public RelayCommand SaveCommand => new RelayCommand(execute => UpdateEstate(), canExecute => CanSave());
        public RelayCommand ClearCommand => new RelayCommand(execute => ClearAll(), canExecute => CanSave());

        public RelayCommand ImagePickerCommand => new RelayCommand(execute => OpenImagePicker());

        public ApartmentViewModel() 
        {
            Countries = new ObservableCollection<string>();

            estateManager = EstateManager.GetInstance();

            //EstateManager.ToStringList();

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
                TextBoxValuePrice = _selectedEstate?.Price ?? 0;
                if (_selectedEstate is Residential apartment)
                {
                    TextBoxValueArea = apartment.SqrM;
                    TextBoxValueRooms = apartment.Rooms;
                    TextBoxValueSqrM = apartment.SqrM;
                }
                else
                {
                    TextBoxValueRooms = 0;
                    TextBoxValueSqrM = 0;
                
                }

                OnPropertyChanged(nameof(TextBoxValueStreetName));
                OnPropertyChanged(nameof(TextBoxValueZipCode));
                OnPropertyChanged(nameof(TextBoxValueCity));
                OnPropertyChanged(nameof(TextBoxValueCountry));
                OnPropertyChanged(nameof(TextBoxValueRooms));
                OnPropertyChanged(nameof(TextBoxValueSqrM));
                OnPropertyChanged(nameof(TextBoxValuePrice));
  
        }


        private Estate _selectedEstate;
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

        private int _selectedIndex;
        public int SelectedIndex
        {

            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
                UpdateTextBoxValues();

            }
        }

        public void Reset()
        {
            SelectedEstate = null;
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
            Estate newEstate = new Apartment(
                TextBoxValueStreetName,
                TextBoxValueZipCode,
                TextBoxValueCity,
                TextBoxValueCountry,
                TextBoxValueRooms,
                TextBoxValueSqrM,
                TextBoxValuePrice
            );

            estateManager.Add(newEstate);

            UpdateTextBoxValues();
            Reset();

        }

        public void DeleteEstate()
        {
            if (SelectedEstate != null)
            {
                estateManager.DeleteAt(SelectedIndex);
            }

        }

        public void UpdateEstate()
        {
            if (SelectedEstate != null) 
            {
                if (SelectedImage != null)
                {
                   
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)SelectedImage));

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        encoder.Save(memoryStream);
                        byte[] imageBytes = memoryStream.ToArray();
                    }
                }
                    var isUpdated = EstateList.Update(SelectedEstate, TextBoxValueStreetName, TextBoxValueZipCode, TextBoxValueCity, TextBoxValueCountry, TextBoxValueArea, imageBytes, TextBoxValueRooms, TextBoxValueSqrM, TextBoxValuePrice); 
                if (isUpdated) 
                {
                    OnPropertyChanged(nameof(EstateManager));
                }
            }
        }

        public void ClearAll()
        {
            estateManager.ChangeAt(SelectedEstate, SelectedEstate.Id);
            //EstateManager.GetAt(SelectedEstate.Id);
            //EstateManager.CheckIndex(SelectedEstate.Id);
            //EstateManager.DeleteAll();
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

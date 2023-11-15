using Microsoft.Win32;
using Modern_Real_Estate.Model;
using Modern_Real_Estate.Model.EstateTypes;
using Modern_Real_Estate.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Modern_Real_Estate.Model.EstateTypes;

namespace Modern_Real_Estate.ViewModel
{
    public class TownhouseViewModel : ViewModelBase
    {
        public EstateManager estateManager { get; set; }
        public ObservableCollection<string> Countries { get; set; }
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


        public RelayCommand AddCommand => new RelayCommand(execute => AddEstate());
        public RelayCommand DeleteCommand => new RelayCommand(execute => DeleteEstate(), canExecute => SelectedEstate != null);
        public RelayCommand SaveCommand => new RelayCommand(execute => UpdateEstate(), canExecute => CanSave());
        public RelayCommand ImagePickerCommand => new RelayCommand(execute => OpenImagePicker());


        public TownhouseViewModel()
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
            if (_selectedEstate is Residential townhouse)
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


            OnPropertyChanged(nameof(TextBoxValueStreetName));
            OnPropertyChanged(nameof(TextBoxValueZipCode));
            OnPropertyChanged(nameof(TextBoxValueCity));
            OnPropertyChanged(nameof(TextBoxValueCountry));
            OnPropertyChanged(nameof(TextBoxValueArea));
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
                if (_selectedEstate != value)
                {
                    _selectedEstate = value;
                    OnPropertyChanged(nameof(SelectedEstate));
                    UpdateTextBoxValues();
                }
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
            Estate newEstate = new Townhouse(
                TextBoxValueStreetName,
                TextBoxValueZipCode,
                TextBoxValueCity,
                TextBoxValueCountry,
                TextBoxValueArea,
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

                Estate newEstate = new Townhouse(
                   TextBoxValueStreetName,
                   TextBoxValueZipCode,
                   TextBoxValueCity,
                   TextBoxValueCountry,
                   TextBoxValueArea,
                   TextBoxValueRooms,
                   TextBoxValueSqrM,
                   TextBoxValuePrice
               );

                var isUpdated = estateManager.ChangeAt(newEstate, SelectedIndex);
                if (isUpdated)
                {
                    OnPropertyChanged(nameof(estateManager));
                }
            }
        }

        public void ClearAll()
        {
            estateManager.DeleteAll();
        }

        private bool CanSave()
        {
            return true;
        }

        public byte[]? imageBytes { get; private set; }

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

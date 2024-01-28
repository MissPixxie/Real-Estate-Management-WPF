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


namespace Modern_Real_Estate.ViewModel
{
    public class StorageViewModel : ViewModelBase
    {
        public EstateManager estateManager { get; set; }
        public ObservableCollection<string> Countries { get; set; }

        public int TextBoxValueId { get; set; }
        public string TextBoxValueStreetName { get; set; } = "";
        public int TextBoxValueZipCode { get; set; } = 0;
        public string TextBoxValueCity { get; set; } = "";
        public string TextBoxValueCountry { get; set; } = "";
        public int TextBoxValueArea { get; set; } = 0;
        public int TextBoxValueRooms { get; set; } = 0;
        public int TextBoxValueSqrM { get; set; } = 0;
        public string TextBoxValueType { get; }
        public decimal TextBoxValuePrice { get; set; } = 0;


        public RelayCommand AddCommand => new RelayCommand(execute => AddEstate());
        public RelayCommand DeleteCommand => new RelayCommand(execute => DeleteEstate(), canExecute => SelectedEstate != null);
        public RelayCommand SaveCommand => new RelayCommand(execute => UpdateEstate(), canExecute => CanSave());
        public RelayCommand ClearCommand => new RelayCommand(execute => ClearAll(), canExecute => CanSave());

        public RelayCommand ImagePickerCommand => new RelayCommand(execute => OpenImagePicker());

        public StorageViewModel()
        {
            Countries = new ObservableCollection<string>();

            estateManager = EstateManager.GetInstance();


            SetCountry();

            UpdateTextBoxValues();
            Reset();
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
                int selectedIndex = EstateManager.MyList.IndexOf(SelectedEstate);
                estateManager.DeleteAt(selectedIndex);
            }
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex = EstateManager.MyList.IndexOf(SelectedEstate); }
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

                Estate newEstate = new Apartment(
                   TextBoxValueStreetName,
                   TextBoxValueZipCode,
                   TextBoxValueCity,
                   TextBoxValueCountry,
                   TextBoxValueRooms,
                   TextBoxValueSqrM,
                   TextBoxValuePrice
               );

                var isUpdated = estateManager.ChangeAt(newEstate, _selectedIndex);
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


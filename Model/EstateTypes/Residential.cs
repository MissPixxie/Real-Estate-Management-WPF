using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using Modern_Real_Estate.Utilities;
using Modern_Real_Estate.ViewModel;

namespace Modern_Real_Estate.Model.EstateTypes
{
    [XmlInclude(typeof(Residential))]
    public abstract class Residential : Estate
    {
        private string _type;
        public string Type
        {
            get { return "Residential"; }
        }

        private int _rooms;
        public int Rooms
        {
            get { return _rooms; }
            set
            {
                _rooms = value;
                OnPropertyChanged(nameof(Rooms));
            }
        }


        private int _sqrM;
        public int SqrM
        {
            get { return _sqrM; }
            set
            {
                _sqrM = value;
                OnPropertyChanged(nameof(SqrM));
            }
        }
    }
}

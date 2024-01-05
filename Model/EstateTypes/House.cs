using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Modern_Real_Estate.Model.EstateTypes
{

    [XmlInclude(typeof(House))]
    public abstract class House : Residential, IHouse
    {
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
    }
}

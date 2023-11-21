using Modern_Real_Estate.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.IO;
using System.Runtime.Serialization;
using Modern_Real_Estate.Model.EstateTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Modern_Real_Estate.Model
{
    [Serializable]
    public class ListManager<T> : IListManager<T>
    {

        private static ObservableList<T> _myList = new ObservableList<T>();
        public static ObservableList<T> MyList { get { return _myList; } }

        //private static ObservableCollection<T> _myList = new();

        //public ObservableList<string> StringList { get; set; }

        private ObservableList<string> _stringList = new ObservableList<string>();
        public ObservableList<string> StringList { get { return _stringList; } }

        public int Count { get; }

        // Responsibility add item på list
        public bool Add(T aType)
        {
            if (aType != null)
            {
                _myList.Add(aType);
                return true;
            }
            else throw new ArgumentException();
        }


        // Responsibility delete item at index
        public void DeleteAt(int index)
        {
           
            if (CheckIndex(index))
            {    
                _myList.RemoveAtIndex(index);               
            }
        }

        public bool ChangeAt(T aType, int index)
        {
            string errorMsg = null;

            try
            {
                if (CheckIndex(index) && (aType != null))
                {
                    _myList.ChangeAt(aType, index);
                    return true;
                }
            }
            catch ( Exception ex)
            {
                errorMsg = ex.Message;
             
            }
            return false;
        }

        public T GetAt(int index)
        {

            if((index >= 0) && (index < _myList.Count))
            {
                return _myList[index];
            }
            return default(T);
        }

        public bool CheckIndex(int index)
        {
            if (index >= 0 && index <= _myList.Count)
            {
                return true;
            }
            else
            {
                return false; 
            }
        }

        // Responsibility delete object
        public void DeleteAll()
        {
            _myList.Clear();
        }

        // Responsibility make a list of strings from the _mylist
        public List<string> ToStringList()
        {
            StringList.Clear();
            foreach ( T item in _myList)
            {
                var itemString = item.ToString();
                StringList.Add(itemString);
            }

            return StringList;
        }

        public string[] ToStringArray()
        {
            var list = ToStringList();
            var arrayList = list.ToArray();

            return arrayList;
        }

        public bool Serialize(string filePath)
        {
            string errorMsg = null;
            try
            {
                string? jsonString = JsonConvert.SerializeObject(_myList, options);
                File.WriteAllText(filePath, jsonString);
                return true;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return false;
            }
        }

        public void DeSerialize(string filePath)
        {
            string errorMsg = null;

            try
            {
                string? jsonString = File.ReadAllText(filePath);
                var test = JsonConvert.DeserializeObject<ObservableList<T>>(jsonString, options);

                foreach (var item in test)
                {
                    _myList.Add(item);
                }

            }
            catch (Exception ex)
            { 
                errorMsg = ex.Message;
            }
        }

        JsonSerializerSettings options = new JsonSerializerSettings
        {
            Formatting = Newtonsoft.Json.Formatting.Indented,
            TypeNameHandling = TypeNameHandling.Auto,
            Converters = { new EstateConverter()}
        };

        public void XMLSerialize(string filePath)
        {

            XMLSerialization serialization = new XMLSerialization();
            serialization.SerializeToFile(_myList, filePath);
        }

        public void XMLDeSerialize(string filePath)
        {
            XMLSerialization serialization = new XMLSerialization();
            serialization.DeserializeFromFile<T>(filePath);
        }
    }

}

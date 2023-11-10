using Modern_Real_Estate.Model;
using Modern_Real_Estate.Model.EstateTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Modern_Real_Estate.Utilities
{
    public class XMLSerialization
    {

        public bool SerializeToFile<T>(T obj, string filePath)
        {

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            TextWriter writer = new StreamWriter(filePath);
            
            serializer.Serialize(writer, obj);
            writer.Close();
            return true;

        }

        public T DeserializeFromFile<T>(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            object obj = null;
            string errorMsg = null;

            TextReader reader = null;

            try
            {
                reader = new StreamReader(filePath);
                obj = (T)serializer.Deserialize(reader);
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return (T)obj;
        }
    }
}

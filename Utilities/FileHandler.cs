using Modern_Real_Estate.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace Modern_Real_Estate.Utilities
{
    public class FileHandler
    {
        //public static object list = new EstateManager();
        private string errorMessage = "Could not open file";
        private bool IsSaved = false;


        public void OpenFile(string filePath)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "C://";
            openFileDialog.Filter = "XML (*.xml)|*.xml";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //string filePath = openFileDialog.FileName;

                //using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                //{
                //    byte[] data = new byte[fileStream.Length];

                //    fileStream.Read(data, 0, data.Length);

                //    string text = System.Text.Encoding.UTF8.GetString(data);
                //}
            }        
        }


        public void CreateFile( object list)
        {


            //if (filePath != null)
            //{
            //    TextWriter writer = new StreamWriter(filePath);

                

                //using (StreamWriter writer = File.CreateText(filePath))
                //{
                //    //serializer.Serialize(writer, estates);
                //    writer.WriteLine("Testar en text");
                //    writer.Close();
                //} 
            //}        
        }


        public void SaveFile()
        {
            IsSaved = true;
        }

        public void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}


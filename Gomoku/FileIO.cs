using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Gomoku
{
    /// <summary>
    /// FileIO handels read and write request
    /// </summary>
    internal class FileIO : Settings
    {
        private string defaultPath;

        /*
         *
         * Following path
         * Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
         * Leads to path bellow
         * C:\Users\richa\AppData\Roaming
         *

         */

        // If no path is set (by dev) it will be defaulted to documents
        /// <summary>
        /// Config for the FileIO class
        /// </summary>
        /// <param name="filename">Filename including extention wished to read/write; Default: "unnamed.txt"</param>
        /// <param name="_path">DefaultPath to read/write files from/to; Default: %User%\documents</param>
        public FileIO(string filename = "unnamed.txt",string _path = "none")
        {

            this.defaultPath = _path != "none" ? _path : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            defaultPath += @"\"+ filename;
            System.Diagnostics.Debug.WriteLine(defaultPath);
        }

        /// <summary>
        /// Writes data to seleced Path
        /// </summary>
        /// <param name="data"><see cref="System.String"/>Array contains data in a row-to-index relation data-structure</param>
        public void FileInput(Array dataArr)
        {
            try
            {
                FileAccess access = FileAccess.Write;
                FileMode mode = FileMode.Create;

                FileStream str = new FileStream(defaultPath, mode, access);
                BinaryWriter writer = new BinaryWriter(str);

                foreach(var item in dataArr)
                {
                    writer.Write(item.ToString());
                }
                writer.Dispose();
            } catch(Exception e)
            {
                MessageBox.Show(e.Message, "FileInput error occured");
            }
        }

      /// <summary>
      /// Get file from selecedpath
      /// </summary>
      /// <returns>string array of file row-to-index based</returns>
        public Array FileOutput()
        {            
            try
            {
                
                FileAccess access = FileAccess.Read;
                FileMode mode = FileMode.Open;

                FileStream str = new FileStream(defaultPath, mode, access);
                StreamReader reader = new StreamReader(str);
               
                List<string> list = new List<string>();


                string[] lines = File.ReadAllLines(defaultPath);

                return lines;
            } catch(Exception e)
            {
                MessageBox.Show(e.Message, "FileOutput error occured");
                return null;
            }
        }
    }
}
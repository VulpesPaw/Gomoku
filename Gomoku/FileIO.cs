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
    internal class FileIO
    {
        /// <summary>
        /// Writes data to seleced Path, if dir does not exit, it will be created
        /// </summary>
        /// <param name="data"><see cref="System.String"/>Array contains data in a row-to-index relation data-structure</param>
        /// <param name="dirPath"><see cref="System.String"/>dirPath are directory and filename</params>
        // If no path is set (by dev) it will be defaulted to documents
        public void FileInput(Array dataArr, string @dirPath, string @filename)
        {
            try
            {
                // creates a dirpath if it doesnt exist
                createDir(dirPath);

                dirPath = Path.Combine(dirPath, filename);
                FileAccess access = FileAccess.Write;
                FileMode mode = FileMode.Create;

                FileStream str = new FileStream(dirPath, mode, access);
                StreamWriter writer = new StreamWriter(str);

                foreach(var item in dataArr)
                {
                    writer.WriteLine(item.ToString());
                }
                writer.Dispose();
            } catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message, "FileInput error occured");
            }
        }

        /// <summary>
        /// Get file from selecedpath
        /// </summary>
        /// <returns>string array of file row-to-index based</returns>
        /// /// <param name="dirPath"><see cref="System.String"/>dirPath are directory and filename</params>
        /// <param name="dirPath"><see cref="System.String"/>dirPath are directory and filename</params>

        public string[] FileOutput(string @dirPath, string @filename)
        {
            try
            {
                if(!checkForDir(dirPath))
                {
                    throw new ArgumentException("Directorypath or file does not exist!", dirPath);
                }
                dirPath = Path.Combine(dirPath, filename);
                System.Diagnostics.Debug.Write(dirPath);

                FileAccess access = FileAccess.Read;
                FileMode mode = FileMode.Open;

                FileStream str = new FileStream(dirPath, mode, access);
                StreamReader reader = new StreamReader(str);

                string[] lines = File.ReadAllLines(dirPath);

                reader.Dispose();

                return lines;
            } catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message + "FileOutput error occured");
                return null;
            }
        }

        /// <summary>
        /// Creates directory if it does not exist
        /// </summary>
        /// <param name="dirPath">Directory path</param>
        /// <returns>Boolean; True: if succsessful and directory does not already exist
        /// (Bool)False: if dir already exist</return>
        public bool createDir(string @dirPath)
        {
            try
            {
                if(!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                    return true;
                } else
                {
                    return false;
                }
            } catch(Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Standalone function to check for directory
        /// </summary>
        /// <param name="dirPath">Directory to check</param>
        /// <returns>True if dir exists, else False</returns>
        public bool checkForDir(string @dirPath)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine(dirPath);
                if(Directory.Exists(dirPath) && dirPath != null)
                {
                    return true;
                }
            } catch(Exception)
            {
            }
            return false;
        }
    }
}
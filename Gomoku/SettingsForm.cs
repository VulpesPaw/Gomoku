using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Gomoku
{
    public partial class SettingsForm : Form
    {
        private string @fullPath;
        public string gameTag, darkmode;
        public string @dirPath, filename;
        private FileIO fileHandler = new FileIO();

        public SettingsForm(string @_dirPath, string _filename)
        {
            try
            {
                InitializeComponent();
                this.@fullPath = Path.Combine(_dirPath, _filename);
                this.dirPath = _dirPath;
                this.filename = _filename;

                // Applies inital settings if present
                bool dirExist = fileHandler.checkForDir(dirPath);
                if(dirExist)
                {
                    string[] settings = fileHandler.FileOutput(dirPath, filename);
                    gameTag = settings[0];
                    darkmode = settings[1];
                    tbxGameTag.Text = settings[0];
                    if(settings[1] == "True")
                    {
                        cbxDM.Checked = true;
                    } else
                    {
                        cbxDM.Checked = false;
                    }
                }
            } catch(Exception)
            {
            }
        }

        /// <summary>
        /// Gets settings
        /// </summary>
        /// <param name="_dirPath"></param>
        /*public SettingsForm(string @_dirPath)
        {
            // Applies inital settings if present
            bool dirExist = fileHandler.checkForDir(@_dirPath);
            System.Diagnostics.Debug.WriteLine(@_dirPath);
            if(dirExist)
            {
                string[] settings = fileHandler.FileOutput(@_dirPath ,filename);
                gameTag = settings[0];
                darkmode = settings[1];
            }
        }**/

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(String.IsNullOrWhiteSpace(tbxGameTag.Text))
                {
                    gameTag = "";
                } else
                {
                    gameTag = tbxGameTag.Text;
                }
                darkmode = cbxDM.Checked.ToString();
                string[] settings = { gameTag, darkmode };

                fileHandler.FileInput(settings, dirPath, filename);
                System.Diagnostics.Debug.WriteLine("OK?");
                DialogResult = DialogResult.OK;
            } catch(Exception)
            {
            }
        }

        /// <summary>
        /// Gets settings
        /// </summary>
        /// <param name="dirPath">The directory paht exluding filename and extion</param>
        /// <param name="filename">The filename including extion</param>
        /// <returns>Boolean: True if settings was found, false if not</returns>
        public bool getSettings(string @dirPath, string filename)
        {
            try
            {
                bool dirExist = fileHandler.checkForDir(dirPath);
                if(dirExist)
                {
                    string[] settings = fileHandler.FileOutput(dirPath, filename);
                    gameTag = settings[0];
                    darkmode = settings[1];

                    return true;
                }
            } catch(Exception)
            {
            }
            return false;
        }
    }
}
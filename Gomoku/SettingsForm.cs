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
        private string @dirPath, filename;
        private FileIO fileHandler = new FileIO();

        public SettingsForm(string @_dirPath, string _filename)
        {
            InitializeComponent();
            this.@fullPath = Path.Combine(_dirPath, _filename);
            this.dirPath = _dirPath;
            this.filename = _filename;

            // Applies inital settings if present
            bool dirExist = fileHandler.checkForDir(fullPath);
            if(dirExist)
            {
                string[] settings = fileHandler.FileOutput(fullPath);
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
        }

        /// <summary>
        /// Gets settings
        /// </summary>
        /// <param name="_dirPath"></param>
        public SettingsForm(string @_dirPath)
        {
            // Applies inital settings if present
            bool dirExist = fileHandler.checkForDir(@_dirPath);
            System.Diagnostics.Debug.WriteLine(@_dirPath);
            if(dirExist)
            {
                string[] settings = fileHandler.FileOutput(@_dirPath);
                gameTag = settings[0];
                darkmode = settings[1];
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
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

            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Gets settings
        /// </summary>
        /// <param name="fullPath">The full paht including filename and extion</param>
        /// <returns>Boolean: True if settings was found, false if not</returns>
        public bool getSettings(string @fullPath)
        {
            bool dirExist = fileHandler.checkForDir(fullPath);
            if(dirExist)
            {
                string[] settings = fileHandler.FileOutput(fullPath);
                gameTag = settings[0];
                darkmode = settings[1];

                return true;
            }
            return false;
        }
    }
}
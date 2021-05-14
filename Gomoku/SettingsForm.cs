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
            InitializeComponent();
            this.@fullPath = Path.Combine(_dirPath, _filename);
            this.dirPath = _dirPath;
            this.filename = _filename;



        }

        private void Settings_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Before settings luanch");

            bool dirExist = fileHandler.checkForDir(fullPath);
            if(dirExist)
            {
                System.Diagnostics.Debug.WriteLine("Before settings luanch");

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
          //  DialogResult = DialogResult.Cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(tbxGameTag.Text))
            {
                return;
            }
            gameTag = tbxGameTag.Text;
            darkmode = cbxDM.Checked.ToString();
            string[] settings = { gameTag, darkmode };

            fileHandler.FileInput(settings, dirPath,filename);
            //settings = fileHandler.FileOutput(fullPath);
            // System.Diagnostics.Debug.WriteLine(settings.Length);
            // System.Diagnostics.Debug.WriteLine(settings);

            DialogResult = DialogResult.OK;

            //this.Close();

            /* TODO
             * Check and read current applied settings
             * Applay current settings to form-screen
             * Save settings on 'save'-press
             * When incorrectly filled, prompt!
             *
             *
             */
        }
    }
}
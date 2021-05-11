using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gomoku
{
    public partial class SettingsForm : Form
    {

        public string gameTag, darkmode;
        FileIO fileHandler = new FileIO("settings.lol");
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(tbxGameTag.Text))
            {
                return;
            }
            gameTag = tbxGameTag.Text;
            darkmode = cbxDM.Checked.ToString();
            string[] settings = { gameTag, darkmode};

            fileHandler.FileInput(settings);
            settings = fileHandler.FileOutput();
            System.Diagnostics.Debug.WriteLine(settings.Length);
            System.Diagnostics.Debug.WriteLine(settings);

            this.Close();

            

            /* TODO
             * Check and read current applied settings 
             * Applay current settings to form-screen
             * Save settings on 'save'-press
             * Close settings window with this.close();
             * 
             */
        }
    }
}

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
    public partial class Form1 : Form
    {
        /* TODO:
         *  ▼ File IO
         *  • Settings
         *  • Network
         *  • GameRules
         *
         */

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // This essentially creates a class, known as pg (type ServerForm)
            ServerForm pg = new ServerForm();

            pg.Show();

            //if()
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // this.Hide();
            string @path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            System.Diagnostics.Debug.WriteLine(path);
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            HostOptionsForm settingsForm = new HostOptionsForm();
            DialogResult r = HostOptionsForm.ShowDialog();
            if(r == DialogResult.Yes)
            {
                //Server is selected
            }else if (r == DialogResult.No)
            {
                //Client is selected
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            string @documentPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            SettingsForm settingsForm = new SettingsForm(@documentPath + @"\c-gomoku", "settings.hc");

            DialogResult r = settingsForm.ShowDialog();

            System.Diagnostics.Debug.WriteLine(r);

            if(r == DialogResult.OK)
            {
                loadSettings(settingsForm);
            }
        }

        private void loadSettings(SettingsForm settings)
        {
            if(settings.darkmode == "True")
            {
                this.BackColor = Color.Black;
                this.ForeColor = Color.White;
                foreach(Control c in this.Controls)
                {
                    UpdateColorControls(c, Color.Black, Color.White);
                }
            } else
            {
                this.BackColor = SystemColors.Control;
                this.ForeColor = SystemColors.ControlText;
                foreach(Control c in this.Controls)
                {
                    UpdateColorControls(c, SystemColors.Control, SystemColors.ControlText);
                }
            }
        }

        // Exits application
        private void btnExit_Click(object sender, EventArgs e)
        {
            // Code 0 are default exit
            Environment.Exit(0);
        }

        public void UpdateColorControls(Control myControl, Color bg, Color fg)
        {
            myControl.BackColor = bg;
            myControl.ForeColor = fg;
            foreach(Control subC in myControl.Controls)
            {
                UpdateColorControls(subC, bg, fg);
            }
        }
    }
}
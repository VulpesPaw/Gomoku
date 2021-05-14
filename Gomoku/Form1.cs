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
    public partial class Form1 : Form
    {
        /* TODO:
         *  ▼ File IO
         *  • Settings
         *  • Network
         *  • GameRules
         *
         */
        private string @settingsPath = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"c-gomoku");

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // This essentially creates a class, known as pg (type ServerForm)
          

            //if()
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // this.Hide();
            // string @path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            // System.Diagnostics.Debug.WriteLine(path);

            MessageBox.Show("Random ass message, lol!");
            string @path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            System.Diagnostics.Debug.WriteLine(path);

            Task.Run(() =>
            {
                var dialogResult = MessageBox.Show("Message", "Title", MessageBoxButtons.OKCancel);
                if(dialogResult == DialogResult.OK)
                    MessageBox.Show("OK Clicked");
                else
                    MessageBox.Show("Cancel Clicked");
            });

        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            HostOptionsForm hostOptions = new HostOptionsForm();
            DialogResult r = hostOptions.ShowDialog();
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
            SettingsForm settingsForm = new SettingsForm(settingsPath, "settings.hc");

            DialogResult r = settingsForm.ShowDialog();

            System.Diagnostics.Debug.WriteLine(r);

            if(r == DialogResult.OK)
            {
                applySettings(settingsForm);
            }
        }

        private void applySettings(SettingsForm settings)
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

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\c-gomoku\settings.hc";
                System.Diagnostics.Debug.WriteLine(path);
                SettingsForm settings = new SettingsForm(path);
                bool gotSettings = settings.getSettings(path);

                if(gotSettings)
                {
                    applySettings(settings);
                }
            } catch(Exception err)
            {
                MessageBox.Show(err.Message, "Error: Damnation to you!");
            }
        }
        private void initiateServer()
        {
            NetworkServer netServer = new NetworkServer();
        }
    }
}
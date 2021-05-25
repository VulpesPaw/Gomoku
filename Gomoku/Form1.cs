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
using System.Net;
using System.Net.Sockets;

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
        private NetworkClient client = new NetworkClient();

        private enum e_active
        {
            server = 0,
            client = 1,
        }

        private e_active active;

        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(600, 600);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // This essentially creates a class, known as pg (type ServerForm)

            //MessageBox.Show("LOL", Text);

            //if()
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // this.Hide();
            // string @path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            // System.Diagnostics.Debug.WriteLine(path);
            /*
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
            */
            client.sendData("Frick");
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            HostOptionsForm hostOptions = new HostOptionsForm();
            DialogResult r = hostOptions.ShowDialog();
            if(r == DialogResult.Yes)
            {
                //Server is selected

                initiateServer();
            } else if(r == DialogResult.No)
            {
                //Client is selected
                initiateClient();
            }
        }

        private void initiateServer()
        {
            try
            {
                // Keep track if player is server or client
                active = e_active.server;

                NetworkServer server = new NetworkServer();
                // random between 1 or 2
                if(new Random().Next(0, 2) > 0)
                {
                    // true means server starts
                }

                /* TODO:
                 * Initate game, send prompt to client
                 * Draw who'll start (50/50?)
                 * Keep track of boards
                 * Change visisbility between groupboxes!
                 *
                 * Add pictures to picBoxes,
                 * On click, call function, applay player symbol. If it is alrady filled, do nothing.
                 * Call oposing player of change, tell its thier turn.
                 *
                 */
            } catch(Exception err)
            {
                MessageBox.Show(err.Message, Text);
            }
        }

        private void initiateClient()
        {
            try
            {
                // Keep track if player is server or client
                active = e_active.client;
                NetClientUI clientUI = new NetClientUI();
                DialogResult rClient = clientUI.ShowDialog();

                if(rClient == DialogResult.OK)
                {
                    client = new NetworkClient(clientUI.Ip);
                    client.connectClientAndResponse();
                } else if(rClient == DialogResult.Cancel)
                {
                    return;
                }
            } catch(Exception err)
            {
                MessageBox.Show(err.Message, Text);
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        public void chosenGrid(int girdNr)
        {
            if(active == 0)
            {
                //TODO Activate grid number
                //server did
            } else
            {
                //client did
            }
        }
    }
}
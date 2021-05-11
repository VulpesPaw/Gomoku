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
        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ServerForm pg = new ServerForm();

            pg.Show();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            ServerForm pg = new ServerForm();
      
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm();
            form.Show();
        }

        // Exits application
        private void btnExit_Click(object sender, EventArgs e)
        {
            // Code 0 are default exit
            Environment.Exit(0);
        }
    }
}

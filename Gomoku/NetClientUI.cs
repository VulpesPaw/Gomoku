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
    public partial class NetClientUI : Form
    {
        private string ip;

        public NetClientUI()
        {
            InitializeComponent();
        }

        public string Ip
        {
            get { return ip; }
            set { ip = value; }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Cancel
            DialogResult = DialogResult.Cancel;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            // Connect
            DialogResult = DialogResult.OK;
        }
    }
}
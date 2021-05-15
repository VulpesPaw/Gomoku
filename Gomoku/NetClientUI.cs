using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace Gomoku
{
    public partial class NetClientUI : Form
    {
        /* TODO
         * • ip validation
         * 
         */

        // IP validation
        //https://stackoverflow.com/questions/11412956/what-is-the-best-way-of-validating-an-ip-address

        private IPAddress ip;

        public NetClientUI()
        {
            InitializeComponent();
        }

        public IPAddress Ip
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
            ip = IPAddress.Parse( tbxIP.Text);
            DialogResult = DialogResult.OK;
        }
    }
}
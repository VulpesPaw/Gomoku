﻿using System;
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
    public partial class HostOptionsForm : Form
    {
        public HostOptionsForm()
        {
            InitializeComponent();
        }

        private void btnServer_Click(object sender, EventArgs e)
        {
            // Player chose Server
            DialogResult = DialogResult.Yes;
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            // Player chose Client
            DialogResult = DialogResult.No;
        }
    }
}

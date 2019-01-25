﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotVision
{
    public partial class Help : Form
    {
        string filename = Environment.CurrentDirectory + @"\Source\help.rtf";

        public Help()
        {
            InitializeComponent();
        }

        private void Help_Load(object sender, EventArgs e)
        {
            richTextBox1.LoadFile(filename);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

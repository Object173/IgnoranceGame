using System;
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
    public partial class EditNewForm : Form
    {
        public EditNewForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Edit frm = (Edit)this.Owner;
            frm.CreateMaps((int)numericUpDown1.Value, (int)numericUpDown2.Value);
            Close();
        }
    }
}

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

namespace NotVision
{
    public partial class EditOpenForm : Form
    {
        public EditOpenForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EditOpenForm_Load(object sender, EventArgs e)
        {
            string[] dirs = Directory.GetFiles(Environment.CurrentDirectory+@"\Source\maps\");
            foreach (string str in dirs)
            {
                string[] s = str.Split('\\');
                listBox1.Items.Add(s[s.Length-1]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Edit frm = (Edit)this.Owner;
            frm.OpenMap(listBox1.Items[listBox1.SelectedIndex].ToString());
            Close();
        }
    }
}

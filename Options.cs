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
    public partial class Options : Form
    {
        string filename;

        public Options()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Options_Load(object sender, EventArgs e)
        {
            filename = Environment.CurrentDirectory + @"\Source\options.pop";

            StreamReader reader = new StreamReader(filename);
            string str;
            str=reader.ReadLine();
            comboBox1.SelectedIndex = int.Parse(str);
            str = reader.ReadLine();
            str = reader.ReadLine();
            if (int.Parse(str) == 1) checkBoxWindow.Checked = true;
            reader.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            StreamWriter writer = new StreamWriter(filename, false);

            writer.WriteLine(comboBox1.SelectedIndex.ToString());
            writer.WriteLine(comboBox1.Items[comboBox1.SelectedIndex]);
            if (checkBoxWindow.Checked) writer.WriteLine("1");
            else writer.WriteLine("0");
            writer.Close();

            Close();
        }
    }
}

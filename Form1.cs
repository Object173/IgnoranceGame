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
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonOptions_Click(object sender, EventArgs e)
        {
            Options form = new Options();
            form.ShowDialog();
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            Help form = new Help();
            form.ShowDialog();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            Edit form = new Edit();
            form.ShowDialog();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            Bitmap logo = new Bitmap(Image.FromFile(Environment.CurrentDirectory + @"\Source\texture\logo.png"),
                                        pictureBox1.Width,pictureBox1.Height);
            logo.MakeTransparent();
            pictureBox1.Image = logo;
        }

        private void buttonGame_Click(object sender, EventArgs e)
        {
            GameMenu form = new GameMenu();
            form.Owner = this;
            form.Show();
            this.Hide();
        }
    }
}

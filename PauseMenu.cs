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
    public partial class PauseMenu : Form
    {
        public PauseMenu()
        {
            InitializeComponent();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            GameForm form = (GameForm)Owner;
            form.Exit();
            Close();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            GameForm game = (GameForm)Owner;
            game.Start();
            game.Show();
            Close();
        }

        private void PauseMenu_Load(object sender, EventArgs e)
        {

        }
    }
}

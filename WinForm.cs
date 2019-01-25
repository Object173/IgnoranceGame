using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotVision
{
    public partial class WinForm : Form
    {
        GameMenu menu;
        Client player;
        Thread netThread;

        int user = 0;

        public WinForm()
        {
            InitializeComponent();
        }

        public WinForm(Form form)
        {
            CallWinPlayer.WinHandler = new CallWinPlayer.WinEvent(this.AddWinner);
            CallWinPlayer.UserHandler = new CallWinPlayer.UserEvent(this.AddPlayer);
            InitializeComponent();
            menu = (GameMenu)form;
        }

        private void WinForm_Load(object sender, EventArgs e)
        {
            GameForm game = (GameForm)Owner;
            player = game.player;

            netThread = new Thread(new ThreadStart(player.Win));
            netThread.IsBackground = true;
            netThread.Start();

            player.showMessage("WIN");
            player.showMessage((DateTime.Now - player.time).ToString());
            player.showMessage(player.kraska.ToString());

            labelName.Text = player.name;
            labelTime.Text = (DateTime.Now - player.time).ToString();
            labelPaint.Text = player.kraska.ToString();

            dataPlayers.Columns.Add( "Col1","Ник игрока");
            dataPlayers.Columns.Add("Col1","Время прохождения");
            dataPlayers.Columns.Add("Col1","Затраченная краска");

            dataPlayers.RowCount = 10;

        }

        public void AddPlayer(string name)
        {
            dataPlayers[0, user].Value = name;
            user++;
        }

        public void AddWinner(int n, string time, string kraska)
        {
            dataPlayers[1, n].Value = time;
            dataPlayers[2, n].Value = kraska;
        }

        private void WinForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            netThread.Abort();
            menu.Close(); 
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            menu.Close(); ;
        }
    }
}

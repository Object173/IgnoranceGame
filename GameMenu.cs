using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotVision
{
    public partial class GameMenu : Form
    {
        public Thread netThread;
        Client player;
        Process server;
        public GameForm game;

        public string statusCl { get { return labelStatusCl.Text; }
                                 set { labelStatusCl.Text = value; }  }
        public string statusSe { get { return labelStatusSe.Text; }
                                 set { labelStatusSe.Text = value; }  }

        public GameMenu()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void GameMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            try {if (player != null) player.Disconnect();}
            catch (Exception exp) { }
            try {if (server != null) server.Kill();}
            catch(Exception exp) {}
            Owner.Show();
        }

        private void GameMenu_Load(object sender, EventArgs e)
        {
            String host = System.Net.Dns.GetHostName();
            System.Net.IPAddress ip = System.Net.Dns.GetHostByName(host).AddressList[0];
            labelip.Text = ip.ToString();

            labelStatusCl.Text = "Не подключен";
            labelStatusSe.Text = "Не запущен";

            string[] dirs = Directory.GetFiles(Environment.CurrentDirectory + @"\Source\maps\");
            foreach (string str in dirs)
            {
                string[] s = str.Split('\\');
                listBoxMaps.Items.Add(s[s.Length - 1]);
            }

            listBoxMaps.SelectedIndex = 0;
        }

        private void buttonOn_Click(object sender, EventArgs e)
        {
            StreamWriter writer = new StreamWriter("server.pop", false);

            writer.WriteLine(numericPortSe.Value);
            writer.WriteLine(Environment.CurrentDirectory + @"\Source\maps\"+listBoxMaps.Items[listBoxMaps.SelectedIndex].ToString());
            writer.WriteLine(numericMaxPlayers.Value);
            writer.Close();

            textBoxIP.Text = labelip.Text;
            numericPortCl.Value = numericPortSe.Value;

            server = Process.Start("server.exe");

            statusSe = "Запущен";

            connectToServer();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            connectToServer();
        }

        public void connectToServer()
        {
            player = new Client(this,(int)numericPortCl.Value,textBoxIP.Text,textBoxName.Text);
            netThread = new Thread(new ThreadStart(player.Run));
            netThread.IsBackground = true;
            netThread.Start();
        }

        private void buttonOff_Click(object sender, EventArgs e)
        {
            try { server.Kill(); }
            catch (Exception exp) { }
            statusSe = "Не запущен";
        }

        private void buttonDis_Click(object sender, EventArgs e)
        {
            try
            {
                player.Disconnect();
            }
            catch(Exception exp) {}
        }

        public void AddUser(string name)
        {
            listBoxPlayers.Items.Add(name);
        }
        public void DelUser(int n)
        {
            listBoxPlayers.Items.RemoveAt(n);
        }
        public void ClearUser()
        {
            listBoxPlayers.Items.Clear();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                player.showMessage("START_SERVER");
            }
            catch(Exception exp) {}
        }

        public void StartGame()
        {
            game = new GameForm(player);
            game.Owner = this;
            player.game = game;
            game.ShowDialog();
        }

        public void showPaint(string str)
        {
            listBoxPlayers.Items.Add(str);
            game.Invoke(new Action<string>((s) => game.PaintAdd(s)), str);
            //game.PaintAdd(str);
            //CallBackMy.paintAddHandler(str);
        }
    }
}

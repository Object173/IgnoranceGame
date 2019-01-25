using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace NotVision
{
    public class Client
    {
        public string name;
        public StreamReader reader;
        public NetworkStream writer;
        int PORT;
        string ip;

        public int PlayerColor;

        TcpClient newClient;
        GameMenu form;
        public GameForm game;
        public Map map;

        public DateTime time;
        public int kraska;

        public Client(GameMenu f, int p, string i, string n)
        {
            form = f;
            PORT = p;
            ip = i;
            name = n;

            kraska = 0;
        }

        public void showMessage(string str)
        {
            str += "\r\n";
            byte[] data = Encoding.ASCII.GetBytes(str);
            writer.Write(data, 0, data.Length);
        }

        public void Run()
        {
            try
            {
                newClient = new TcpClient();

                newClient.Connect(ip, PORT);

                form.Invoke(new Action<string>((str) => form.statusCl = str), "Подключен");

                // создание потоков по чтению и записи     
                writer = newClient.GetStream();
                reader = new StreamReader(newClient.GetStream());

                //отправка ника
                showMessage(name);

                int num = int.Parse(reader.ReadLine());
                PlayerColor = num-(int)(num/8)*8;

                //получение карты
                int len = int.Parse(reader.ReadLine());
                char[] sdata = new char[len];
                reader.ReadBlock(sdata, 0, len);
                byte[] data = Encoding.ASCII.GetBytes(sdata);

                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream ms = new MemoryStream(data);
                map = (Map)formatter.Deserialize(ms);

                string returnData;
                while (true)
                {
                    if (!newClient.Connected)
                        break; //выход 

                    // получение ответа от сервера
                    returnData = reader.ReadLine();

                    if (returnData.IndexOf("USER+") > -1)
                    {
                        returnData = reader.ReadLine();
                        form.Invoke(new Action<string>((str) => form.AddUser(str)), returnData);
                    }
                    if (returnData.IndexOf("USER-") > -1)
                    {
                        returnData = reader.ReadLine();
                        form.Invoke(new Action<int>((n) => form.DelUser(n)), int.Parse(returnData));
                    }
                    if(returnData.IndexOf("PAINT+")>-1)
                    {
                        returnData = reader.ReadLine();
                        form.Invoke(new Action<string>((str) => CallBackMy.paintAddHandler(str)), returnData);
                    }
                    if (returnData.IndexOf("START_GAME") > -1)
                    {
                        form.Invoke(new Action<int>((n) => form.StartGame()), 0);
                    }
                }
                Disconnect();
            }
            catch (Exception exp)
            {
                try
                {
                    form.Invoke(new Action<string>((str) => form.statusCl = str), "ошибка");
                    form.Invoke(new Action<string>((str) => form.ClearUser()), "");
                    form.netThread.Abort();
                }
                catch (Exception ex) { }
            }
        }

        public void Game()
        {
            try
            {
                form.netThread.Suspend();
                time = DateTime.Now;

                string returnData;
                while (true)
                {
                    if (!newClient.Connected)
                        break; //выход 

                    // получение ответа от сервера
                    returnData = reader.ReadLine();

                    if (returnData.IndexOf("PAINT+") > -1)
                    {
                        returnData = reader.ReadLine();
                        form.Invoke(new Action<string>((str) => CallBackMy.paintAddHandler(str)), returnData);
                    }

                }
                Disconnect();
            }
            catch (Exception exp) { }

        }

        public void Win()
        {
            try
            {
                string returnData;

                while (true)
                {
                    returnData = reader.ReadLine();

                    if (returnData.IndexOf("USER+") > -1)
                    {
                        returnData = reader.ReadLine();
                        form.Invoke(new Action<string>((str) => CallWinPlayer.UserHandler(str)), returnData);
                        //win.Invoke(new Action<string>((str) => win.AddPlayer(str)), returnData);
                    }

                    if (returnData.IndexOf("WIN+") > -1)
                    {
                        string t, k; int n;
                        n = int.Parse(reader.ReadLine());
                        t = reader.ReadLine();
                        k = reader.ReadLine();

                        form.Invoke(new Action<int,string,string>((num,str,str2) => CallWinPlayer.WinHandler(num,str,str2)), n,t,k);
                        //win.Invoke(new Action<int, string, string>((num, tim, kras) => win.AddPlayer(num, tim, kras)), n, t, k);
                    }
                }
            }
            catch (Exception exp) { }
        }

        public void Disconnect()
        {
            lock (this)
            {
                newClient.Close();
                form.Invoke(new Action<string>((str) => form.statusCl = str), "Не подключен");
                form.Invoke(new Action<string>((str) => form.ClearUser()), "");
                form.netThread.Abort();
            }
        }
    }
}

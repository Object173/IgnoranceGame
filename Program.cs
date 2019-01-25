using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotVision
{
    public static class CallBackMy
    {
        public delegate void paintAddEvent(string str);
        public static paintAddEvent paintAddHandler;
    }

    public static class CallWinPlayer
    {
        public delegate void UserEvent(string str);
        public delegate void WinEvent(int n, string str, string str2);
        public static UserEvent UserHandler;
        public static WinEvent WinHandler;
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainMenu());
        }

    }
}

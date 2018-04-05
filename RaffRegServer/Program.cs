using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using RaffRegServer.Forms;

namespace RaffRegServer
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new RaffRegServer());
            Login fLogin = new Login();
            fLogin.Show();
            Application.Run();
            /*
            if (fLogin.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new RaffRegServer());
            }
            else
            {
                Application.Exit();
            }*/
            //Application.Run(new Login());
        }
    }
}

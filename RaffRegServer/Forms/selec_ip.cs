using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace RaffRegServer
{
    public partial class selec_ip : Form
    {
        public selec_ip()
        {
            InitializeComponent();
        }

        //public string RetornaIP { get; set; }

        private void selec_ip_Load(object sender, EventArgs e)
        {

            List<string> pIPS = new List<string>();
            // Pegar nome da maquina
            String strHostName = Dns.GetHostName();

            lsIPS.Items.Add("localhost");
            lsIPS.Items.Add(strHostName);
            lsIPS.Items.Add("127.0.0.1");

            // Converter nome do host
            IPHostEntry iphostentry = Dns.GetHostEntry(strHostName);

            // Pegar IP's
            foreach (IPAddress ipaddress in iphostentry.AddressList)
            {

                lsIPS.Items.Add(ipaddress);
                //pIPS.Add(ipaddress.ToString());
            }
        }
        private void retornarSelecao(object sender, EventArgs e)
        {
            //this.RetornaIP = lsIPS.SelectedItem.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
            
        }

        private void cancelaSelecao(object sender, EventArgs e)
        {
            
            this.Close();
        }
    }
}

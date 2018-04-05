using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RaffRegServer.Forms
{
    public partial class Login : Form
    {
        
        public Login()
        {
            InitializeComponent();
            
        }

        string user = "Raffinato";
        static int dia = DateTime.Now.Day;
        static int mes = DateTime.Now.Month;
        static int ano = DateTime.Now.Year;

        int senha = dia + mes + ano;

        private void BtLogin(object sender, EventArgs e)
        {
            //bool parsed = int.TryParse(txtSenha.Text, out int s);
            if (!int.TryParse(txtSenha.Text, out int s))
            {
                MessageBox.Show("A senha é composta apenas por números.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                if (txtUser.Text.Equals(user) && s != senha)
                {
                    MessageBox.Show("Senha Incorreta.\nVerifique a senha e tente novamente.");
                }
                if (!txtUser.Text.Equals(user) && s == senha)
                {
                    MessageBox.Show("Usuário Incorreto.\nVerifique o usuário e tente novamente.");
                }

                if (!txtUser.Text.Equals(user) && s != senha)
                {
                    MessageBox.Show("Usuário e senha Incorretos.\nVerifique os dados e tente novamente.");
                }
                if (txtUser.Text.Equals(user) && s == senha)
                {
                    Form app = new RaffRegServer();
                    app.Show();
                    this.Dispose();

                }
            }
        }
        private void BTFechar(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}

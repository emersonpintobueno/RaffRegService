using System;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;
using RaffRegServer.Classes;


namespace RaffRegServer
{
    
    public partial class LoadForm : Form
    {
        string servico;
        registro reg = new registro();
        public LoadForm(string servico)
        {
            this.servico = servico;
            InitializeComponent();
            Iniciar();
        }

        private void Iniciar()
        {
            Thread t = new Thread(()=>MServico());
            t.Start();
        }

        private ServiceController SC()
        {
            ServiceController sc = new ServiceController();
            try
            {
                sc = new ServiceController(servico);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Serviço do SQL não encontrado");
            }
            return sc;
        }

        

        private void MServico()
        {
            RaffRegServer r = (RaffRegServer)Application.OpenForms["RaffRegServer"];

            ServiceController sc = reg.SC(servico);
            int s = 25;
            ServiceControllerStatus scSt = sc.Status;
            if (sc.Status == ServiceControllerStatus.Running)
            {
                lblStatus.Invoke((MethodInvoker)(() => lblStatus.Text = "Aguarde. Parando o Serviço"));
                bProgresso.Invoke((MethodInvoker)(() => bProgresso.MarqueeAnimationSpeed=s));
                this.Invoke((MethodInvoker)(() => this.Text = "Parando o serviço."));
                try
                {
                    sc.Stop();
                    while (sc.Status != ServiceControllerStatus.Stopped)
                    {
                        lblStatus.Invoke((MethodInvoker)(() => lblStatus.Text = "Parando..."));
                        sc.WaitForStatus(ServiceControllerStatus.Stopped);
                        sc.Refresh();
                    }
                    lblStatus.Invoke((MethodInvoker)(() => lblStatus.Text = "Serviço parado com sucesso."));
                    r.btSQLServico.Invoke((MethodInvoker)(() => r.btSQLServico.Text = "Parado"));
                    this.Invoke((MethodInvoker)(() => this.Dispose()));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro:" + ex);
                }
            }

            else
            {
                if (lblStatus.InvokeRequired)
                {
                    lblStatus.Invoke((MethodInvoker)(() => lblStatus.Text = "Aguarde. Iniciando o Serviço SQL..."));
                }
                else
                {
                    lblStatus.Text = "Aguarde. Iniciando o Serviço SQL...";
                }
                
                bProgresso.Invoke((MethodInvoker)(() => bProgresso.MarqueeAnimationSpeed = s));
                this.Invoke((MethodInvoker)(() => this.Text = "Iniciando o serviço."));
                try
                {
                    sc.Start();
                    while (sc.Status != ServiceControllerStatus.Running)
                    {
                        lblStatus.Invoke((MethodInvoker)(() => lblStatus.Text = "Iniciando..."));
                        sc.WaitForStatus(ServiceControllerStatus.Running);
                        sc.Refresh();
                    }
                    lblStatus.Invoke((MethodInvoker)(() => lblStatus.Text = "Serviço iniciado com sucesso."));
                    r.btSQLServico.Invoke((MethodInvoker)(() => r.btSQLServico.Text = "Iniciado"));
                    this.Invoke((MethodInvoker)(() => this.Dispose()));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro:" + ex);
                }
            }
        }
    }
}

using System;
using System.ServiceProcess;
using System.Windows.Forms;
using RaffRegServer.Classes;


namespace RaffRegServer
{
    public partial class LoadForm : Form
    {
        string servico;
        private static System.Timers.Timer m;
        registro reg = new registro();
        public LoadForm(string servico)
        {
            this.servico = servico;
            InitializeComponent();
            Rel();
            MServico();
        }

        public void Rel()
        {
            m = new System.Timers.Timer();
            m.Interval = 1000;
            m.Elapsed += MTimer;
            m.AutoReset = true;
            m.Enabled = true;
        }

        public void MTimer(Object source, System.Timers.ElapsedEventArgs e)
        {

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
        //private void Monitor(Object source, System.Timers.ElapsedEventArgs e)

        private void MServico()
        {
            ServiceController sc = reg.SC(servico);

            ServiceControllerStatus scSt = sc.Status;

            if (sc.Status == ServiceControllerStatus.Running)
            {
                //lblStatus.Invoke((MethodInvoker)(() => lblStatus.Text = "Aguarde. Parando o Serviço"));
                lblStatus.Text = "Aguarde. Parando o Serviço";
                try
                {
                    sc.Stop();
                    while (sc.Status != ServiceControllerStatus.Stopped)
                    {
                        //lblStatus.Invoke((MethodInvoker)(() => lblStatus.Text = "Parando..."));
                        lblStatus.Text = "Parando...";
                        sc.WaitForStatus(ServiceControllerStatus.Stopped);
                        sc.Refresh();
                    }
                    //lblStatus.Invoke((MethodInvoker)(() => lblStatus.Text = "Serviço Parado com Sucesso!"));
                    lblStatus.Text = "Serviço Parado com Sucesso!";
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro:" + ex);
                }
            }

            else
            {
                //lblStatus.Invoke((MethodInvoker)(() => lblStatus.Text = "Aguarde. Iniciando o Serviço SQL..."));
                lblStatus.Text = "Aguarde. Iniciando o Serviço SQL...";
                try
                {
                    sc.Start();
                    while (sc.Status != ServiceControllerStatus.Running)
                    {
                        //lblStatus.Invoke((MethodInvoker)(() => lblStatus.Text = "Iniciando..."));
                        lblStatus.Text = "Iniciando...";
                        sc.WaitForStatus(ServiceControllerStatus.Running);
                        sc.Refresh();
                    }
                    //lblStatus.Invoke((MethodInvoker)(() => lblStatus.Text = "Serviço iniciado com sucesso."));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro:" + ex);
                }
            }
        }
    }
}

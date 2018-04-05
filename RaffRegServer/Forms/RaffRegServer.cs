using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Diagnostics;
using RaffRegServer.Classes;
using ZipProgress;
using System.Threading;

namespace RaffRegServer
{
    public partial class RaffRegServer : Form
    {
        registro reg = new registro();
        SQL sql = new SQL();
        Computador pc = new Computador();
        string pRaff;

        
        public RaffRegServer()
        {
            try
            {
                InitializeComponent();
                pRaff = PathRaffinato();
                preencheDados();
            }
            catch (Exception ex)
            {
                reg.SalvarLog(ex.ToString());
                MessageBox.Show(ex.ToString(), "Erro ao abrir.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            Application.Exit();
        }

        #region registro (referência apenas)
        /*
            [HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Raffinato 2009\Conexão]
            c1  "Host"="127.0.0.1"
            c2  "HostMatriz"="127.0.0.1"
            c3  "Porta"=dword:00002710
            c4  "PortaMatriz"=dword:00002710
            c5  "TecladoPrompt"=dword:ffffffff
            c6  "Filial"=dword:00000001
            c7  "IdImagem"=dword:00000000
            c8  "MenuWeb"=dword:00000001
            c9  "ModoTouch"=dword:00000000
            c10 "Modulos"="1;3;4;2;"
            c11 "Compactada"=dword:00000001

            [HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Raffinato 2009\Conexão PDA]
            cp1 "Host"="Localhost"
            cp2 "Porta"=dword:00002742

            [HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Raffinato 2009\PAF]
            p1  "Porta"=dword:00002712

            [HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Raffinato 2009\Servidor]
            s1  "DriverSQL"="SQLOLEDB.1"
            s2  "GerarLog"=dword:00000001
            s3  "UsarFiltro"=dword:00000000
            s4  "NomeDataBase"="Raffinato"
            s5  "Host"="BANCADA-III\\SQL2014"
            s6  "NomeServicoSQL"=""
            s7  "PathArquivoDados"="C:\\Raffinato 3.5\\Base Dados\\"
            s8  "Porta"=dword:00002710
            s9  "TecnologiaAcesso"=dword:00000000
            s10 "TempoLimiteMicroTerminal"="1"
            s11 "TimeOutQuery"=dword:000000b4
            s12 "GerarReplicaCentral"=dword:00000000

            [HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Raffinato 2009\Serviço]
            se1 "NomeHost"="Localhost"
            se2 "Porta"=dword:00002711
            se3 "Host"="Localhost"

            [HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Raffinato 2009\Sincronizador]
            si1 "Porta"=dword:00002774
            si2 "Host"="Localhost"
            */
        #endregion

        public void preencheDados()
        {
            string[][] dadosRegistro = new string[30][];
            try
            {
                dadosRegistro = reg.leRegistro();
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao ler as entradas de registro.");
            }

            cmbHosts.Items.Clear();
            //Preenche box dos ips
            List<string> ip = new List<string>();

            try
            {
                ip = reg.CIPs();
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao ler aos ips disponíveis.");
            }

            foreach (string d in ip)
            {
                cmbHosts.Items.Add(d);
            }

            cmbHosts.SelectedIndex = 0;
            //---------------------

            //Esconder aba serviços
            tabMain.TabPages.Remove(tabServ);

            ///<summary>
            ///Nomeação dos campos
            /// </summary>
            txtBHostPDV.Text = dadosRegistro[0][1]; //Host do PDV
            txtBHostPDVPorta.Text = dadosRegistro[1][1]; //Porta do Host do PDV
            txtBFilial.Text = dadosRegistro[2][1]; //Filial
            txtBFilialImg.Text = dadosRegistro[3][1]; //Imagens do Filial
            txtBHostRetaguarda.Text = dadosRegistro[4][1]; //Host Matriz
            txtBHostRetaguardaPorta.Text = txtBSQLPorta.Text = dadosRegistro[5][1]; //Porta do Host da Matriz

            //txtBServPorta = dadosRegistro[5][1]

            if (dadosRegistro[6][1].Equals("1"))
            {
                chTouch.Checked = true;
            }

            if (dadosRegistro[7][1].Equals("1"))
            {
                chMWeb.Checked = true;
            }
            txtBSincHost.Text = dadosRegistro[8][1];
            txtBSincHostPorta.Text = dadosRegistro[9][1];
            txtBSincRep.Text = dadosRegistro[10][1];
            txtBSincRepPorta.Text = dadosRegistro[11][1];
            txtBPAFPorta.Text = dadosRegistro[12][1];
            txtBServHost.Text = dadosRegistro[13][1];
            txtBServPorta.Text = dadosRegistro[14][1];
            txtBTabletHost.Text = dadosRegistro[15][1];
            txtBTabletPorta.Text = dadosRegistro[16][1];

            //Modulos ativos
            //1 = Venda rápida
            if (dadosRegistro[17][1].ToLower().Contains('1'))
            {
                chVendaRapida.Checked = true;
            }
            //2 = Venda tele entrega
            if (dadosRegistro[17][1].ToLower().Contains('2'))
            {
                chTeleEntrega.Checked = true;
            }
            //3 = Venda mesa
            if (dadosRegistro[17][1].ToLower().Contains('3'))
            {
                chVendaMesa.Checked = true;
            }
            //4 = Venda cartão consumo
            if (dadosRegistro[17][1].ToLower().Contains('4'))
            {
                chCartaoConsumo.Checked = true;
            }

            //txtBSQLDrive.Text = dadosRegistro[18][1]; //Drive do SQL

            txtBSQLHost.Text = dadosRegistro[19][1]; //Host do SQL
            txtBSQLServidor.Text = Environment.MachineName; //Servidor

            //Setar ComboBox para as instâncias SQL disponíveis.
            //TODO: Instâncias - fazer futuro tratamento para quando não houver sql instalado.
            foreach (var item in sql.PegarInstSQL())
            {
                int i = item.Length;
                int a = item.IndexOf("\\");
                int b = i - a;
                string h = item.Substring(a + 1, b - 1);
                cmbInstancias.Items.Add(h);
            }
            cmbInstancias.SelectedIndex = 0;

            //preencher combo box com as instâncias do sql caso hajam mais de uma
            //TODO: Preencher combo box dos serviços para quando não se tratar do servidor
            if (sql.SSql().Count > 1)
            {
                for (int i = 0; i < sql.SSql().Count; i++)
                {
                    cmBSQLServico.Items.Add(sql.SSql()[i]);

                }
            }
            else
            {
                cmBSQLServico.Items.Add(sql.SSql()[0]);

            }
            cmBSQLServico.SelectedIndex = 0;


            if (sql.StatusSQL(cmBSQLServico.SelectedItem.ToString()))
            {
                btSQLServico.Text = "Parar";
            }
            else
            {
                btSQLServico.Text = "Iniciar";
            }


            //preencher as bases de dados disponíveis no sql
            cmbSQLArquivo.Items.Add(dadosRegistro[20][1]);
            cmbSQLArquivo.SelectedIndex = 0;

            txtBServMicro.Text = dadosRegistro[21][1];

            txtTimeoutQuery.Text = dadosRegistro[22][1];

            if (dadosRegistro[23][1].Equals("1"))
            {
                chBoxLog.Checked = true;
            }

            if (dadosRegistro[24][1].Equals("1"))
            {
                chBoxFiltros.Checked = true;
            }

            //Campos da Aba de informações:
            //Nome do PC
            txtNomePC.Text = Environment.MachineName;
            txtBoxRede.Text = reg.ShowNetworkInterfaces();

            //campos da aba monitorador
            Monitorador();
            tBRaffinato.Text = pRaff;



        }

        //Monitorador de Serviços
        private void Monitorador()
        {
            Color r = Color.Red;
            Color g = Color.Green;
            string p = "Executando";
            string a = "Parado";
            string iniciar = "Iniciar";
            string parar = "Parar";
            

            if (PegarProcesso("sqlservr"))
            {
                lblPSQL.Text = p;
                lblPSQL.ForeColor = g;
            }
            else
            {
                lblPSQL.Text = a;
                lblPSQL.ForeColor = r;
            }
            
            if (PegarProcesso("Integrador"))
            {
                lblPIntegrador.ForeColor = g;
                lblPIntegrador.Text = p;
                btIntegrador.Text = parar;
            }
            else
            {
                lblPIntegrador.ForeColor = r;
                lblPIntegrador.Text = a;
                btIntegrador.Text = iniciar;
            }

            if (PegarProcesso("QS"))
            {
                lblQuantum.ForeColor = g;
                lblQuantum.Text = p;
                btQuantum.Text = parar;
            }
            else
            {
                lblQuantum.ForeColor = r;
                lblQuantum.Text = a;
                btQuantum.Text = iniciar;
            }
            if (PegarProcesso("Replicador"))
            {
                lblPReplicador.Text = p;
                lblPReplicador.ForeColor = g;
                btReplicador.Text = parar;
            }
            else
            {
                lblPReplicador.Text = a;
                lblPReplicador.ForeColor = r;
                btReplicador.Text = iniciar;
            }
            if (PegarProcesso("Servico"))
            {
                lblPServico.Text = p;
                lblPServico.ForeColor = g;
                btServico.Text = parar;
            }
            else
            {
                lblPServico.Text = a;
                lblPServico.ForeColor = r;
                btServico.Text = iniciar;
            }
            if (PegarProcesso("Servidor"))
            {
                lblPServidor.Text = p;
                lblPServidor.ForeColor = g;
                btServidor.Text = parar;
            }
            else
            {
                lblPServidor.Text = a;
                lblPServidor.ForeColor = r;
                btServidor.Text = iniciar;
            }
            if (PegarProcesso("Sincronizador"))
            {
                lblPSincronizador.Text = p;
                lblPSincronizador.ForeColor = g;
                btSincronizador.Text = parar;
            }
            else
            {
                lblPSincronizador.Text = a;
                lblPSincronizador.ForeColor = r;
                btSincronizador.Text = iniciar;
            }
            if (File.Exists(pRaff+ "\\Integrador.exe"))
            {
                txtIntegrador.Text = pRaff + "\\Integrador.exe";
            }
            else
            {
                txtIntegrador.Text = "Executável Ausente";
            }
            if (File.Exists(@"C:\QS\QS.exe"))
            {
                txtQuantum.Text = "C:\\QS\\QS.exe";
            }
            else
            {
                txtQuantum.Text = "Quantum Não Instalado";
            }
            if (File.Exists(pRaff + "\\Replicador.exe"))
            {
                txtReplicador.Text = pRaff + "\\Replicador.exe";
            }
            else
            {
                txtReplicador.Text = "Executável Ausente";
            }
            if(File.Exists(pRaff + "\\Servico.exe"))
            {
                txtServico.Text = pRaff + "\\Servico.exe";
            }
            else
            {
                txtServico.Text = "Executável Ausente";
            }
            if (File.Exists(pRaff + "\\Servidor.exe"))
            {
                txtServidor.Text = pRaff + "\\Servidor.exe";
            }
            else
            {
                txtServidor.Text = "Executável Ausente";
            }
            if (File.Exists(pRaff + "\\Sincronizador.exe"))
            {
                txtSincronizador.Text = pRaff + "\\Sincronizador.exe";
            }
            else
            {
                txtSincronizador.Text = "Executável Ausente";
            }
        }

        public static void IniciarPrograma(string programa)
        {
            Process p = new Process();
            p.StartInfo.FileName = programa;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = false;
            p.Start();
        }

        public static void PararPrograma(string processo)
        {
            Process[] p = Process.GetProcessesByName(processo);
            foreach (Process proc in p)
            {
                proc.Kill();
            }
            
        }

        //Procura e seta o caminho da pasta Raffinato.
        string PathRaffinato()
        {
            Regex reg = new Regex(@"Raffinato(\s+\d+.+\d)");

            var drivers = DriveInfo.GetDrives()
                   .Where(x => x.DriveType == DriveType.Fixed)
                   .Select(k => k.Name).ToList();


            List<String> dirs = new List<string>();

            for (int i = 0; i < drivers.Count; i++)
            {
                dirs.AddRange(Directory.GetDirectories(drivers[i].ToString())
                                    .Where(path => reg.IsMatch(path))
                                    .ToList());
            }

            if (dirs.Count > 1)
            {
                String cEnt = "-----------------\n";

                for (int i = 0; i < dirs.Count; i++)
                {
                    cEnt += dirs[i].ToString() + "\n";
                }
                cEnt += "-----------------\n";
                MessageBox.Show("Foram encontradas as seguintes pastas do Raffinato.\n" +
                   cEnt + "Na tela a seguir selecione a pasta a ser usada.");
                return procuraPasta.ShowDialog() == DialogResult.OK ? procuraPasta.SelectedPath : string.Empty;
            }
            else
            {
                String pasta = "-----------------\n";
                pasta += dirs[0].ToString() + "\n";
                pasta += "-----------------\n";
                //MessageBox.Show("Raffinato encontrado na pasta\n" + pasta + "e será usado como referência.");
                return dirs[0].ToString();
            }
        }

        //função para marcar desmarcar serviços a serem monitorados
        private void ChTodos_Changed(object sender, EventArgs e)
        {
            if (chTodos.Checked == true)
            {
                chIntegrador.Checked = true;
                chQuantum.Checked = true;
                chReplicador.Checked = true;
                chServico.Checked = true;
                chServidor.Checked = true;
                chSincronizador.Checked = true;
                chSQL.Checked = true;
            }
            else
            {
                chIntegrador.Checked = false;
                chQuantum.Checked = false;
                chReplicador.Checked = false;
                chServico.Checked = false;
                chServidor.Checked = false;
                chSincronizador.Checked = false;
                chSQL.Checked = false;
            }
        }

        //gerenciar serviços e processos
        
        private void ChAtivaMonitorador(object sender, EventArgs e)
        {
            if (chAtivaMon.Checked == true)
            {
                tabMain.TabPages.Add(tabServ);
            }
            else
            {
                tabMain.TabPages.Remove(tabServ);
            }

        }

        public static bool PegarServico(string serviceName)
        {
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController service in services)
            {
                if (service.ServiceName == serviceName)
                    return true;
            }
            return false;
        }

        public static bool PegarProcesso(string processName)
        {
            Process[] processos = Process.GetProcesses();

            foreach (Process p in processos)
            {
                if (p.ProcessName == processName)
                {
                    return true;
                }
            }
            return false;

        }

        //setar ip para todos
        private void BTtip(object sender, EventArgs e)
        {
            string ipServer = cmbHosts.SelectedItem.ToString();

            DialogResult resultado = MessageBox.Show("Você gostaria de usar este endereço para todas as entradas pertinentes?",
                "Important Question",
                MessageBoxButtons.YesNo);

            if (resultado == DialogResult.Yes)
            {
                txtBHostPDV.Text = ipServer;
                txtBHostRetaguarda.Text = ipServer;
                txtBSincHost.Text = ipServer;
                txtBSincRep.Text = ipServer;
                txtBServHost.Text = ipServer;
                txtBTabletHost.Text = ipServer;
                txtBSQLServidor.Text = ipServer;
            }

        }

        public void BtConsultaBases(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("O sistema irá tentar conectar no SQL " +
                "utilizando o usuário e senha padrão.\nDeseja prosseguir?",
                "Atenção",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                List<string> servers = new List<string>();
                try
                {
                    servers = sql.PegarListaBases(txtBSQLHost.Text);
                }
                catch (Exception)
                {
                    DialogResult r = MessageBox.Show("Aparentemente o servidor SQL está parado ou não existe.\n" +
                        "Gostaria de tentar iniciar o serviço do sql?",
                     "Atenção",
                     MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (r == DialogResult.Yes)
                    {
                        try
                        {
                            
                            while (!IPSql())
                            {
                                BTIniciarPararSQL(sender, e);
                            }
                            servers = sql.PegarListaBases(txtBSQLHost.Text);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Não foi possível iniciar o SQL\n" +
                                "O erro retornado foi:\n" + ex, "Erro ao iniciar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //throw;
                        }

                    }
                    else
                    {

                        MessageBox.Show("Não há serviços do sql Rodando.", "Sem Servidor!",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    //throw;
                }
                string s = cmbSQLArquivo.SelectedItem.ToString();
                cmbSQLArquivo.Items.Clear();
                for (int i = 0; i < servers.Count; i++)
                {
                    cmbSQLArquivo.Items.Add(servers[i]);

                    //seta o default server igual ao do registro se houver o mesmo nome
                    if (servers[i] == s)
                    {
                        cmbSQLArquivo.SelectedIndex = i;
                    }

                }

            }
        }

        //Seta o nome do botão de acordo com o status do serviço do sql.
        public bool IPSql()
        {
            string nS = cmBSQLServico.SelectedItem.ToString();
            ServiceController sc = new ServiceController();
            bool st = false;
            try
            {
                sc = new ServiceController(nS);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Serviço do SQL não encontrado");
                st=false;
            }

            if (sc.Status == ServiceControllerStatus.Running)
            {
                btSQLServico.Text = "Executando";
                st = true;
            }

            else
            {
                btSQLServico.Text = "Parado";
                st = false;
            }
            return st;
        }


        private void BTIniciarPararSQL(object sender, EventArgs e)
        {
            IPSql();
            string nS = cmBSQLServico.SelectedItem.ToString();

            LoadForm c = new LoadForm(nS);
            c.StartPosition = FormStartPosition.CenterScreen;
            //c.Invoke((MethodInvoker)(() => c.Show()));
            c.Show();
        }

       private void BTSalvarArquivo(object sender, EventArgs e)
        {
            reg.SalvarArquivoConfiguracoes(Configs());

        }

        private List<string> Configs()
        {
            List<string> dados = new List<string>();
            dados.Add(txtBServHost.Text);                       //0 - Endereço do Serviço 
            dados.Add(txtBServPorta.Text);                      //1 - Porta do Serviço
            dados.Add(txtBPAFPorta.Text);                       //2 - Porta do PAF-ECF
            dados.Add(txtBSincHost.Text);                       //3 - Endereço do Sincronizador
            dados.Add(txtBSincHostPorta.Text);                  //4 - Porta do Sincronizador
            dados.Add(txtBSincRep.Text);                        //5 - Endereço do Replicador
            dados.Add(txtBSincRepPorta.Text);                   //6 - Porta do Replicador
            dados.Add(txtBHostPDV.Text);                        //7 - Endereço do Host do PDV
            dados.Add(txtBHostPDVPorta.Text);                   //8 - Porta do Host do PDV
            dados.Add(txtBHostRetaguarda.Text);                 //9 - Endereço do Host do Retaguarda
            dados.Add(txtBHostRetaguardaPorta.Text);            //10 - Porta do Host do Retaguarda
            dados.Add(txtBFilial.Text);                         //11 - Filial
            dados.Add(txtBFilialImg.Text);                      //12 - Imagens da Filial
            dados.Add(chTouch.CheckState.ToString());           //13 - Utilização do Modo Touch (verdadeiro sim, falso não)
            dados.Add(chMWeb.CheckState.ToString());            //14 - Utilização do menu web
            dados.Add(txtBTabletHost.Text);                     //15 - Endereço para conexão dos tablets
            dados.Add(txtBTabletPorta.Text);                    //16 - Porta para conexão dos tablets
            dados.Add(chVendaRapida.CheckState.ToString());     //17 - Habilitar módulo Venda Rápida
            dados.Add(chVendaMesa.CheckState.ToString());       //18 - Habilitar módulo Venda Mesa
            dados.Add(chTeleEntrega.CheckState.ToString());     //19 - Habilitar módulo Tele-Entrega
            dados.Add(chCartaoConsumo.CheckState.ToString());   //20 - Habilitar módulo Cartão Consumo
            dados.Add(txtBSQLHost.Text);                        //21 - Host do Servidor do SQL (lido do registro).
            dados.Add(txtBSQLPorta.Text);                       //22 - Porta do Servidor SQL (lido do registro).
            dados.Add(txtBSQLServidor.Text);                    //23 - Servidor -Host da máquina- (lido da máquina).
            dados.Add(cmbInstancias.SelectedItem.ToString());   //24 - Instâncias de SQL descobertas no computador local.
            dados.Add(cmBSQLServico.SelectedItem.ToString());   //25 - Nome do serviço do SQL descoberto no computador local.
            dados.Add(cmbSQLArquivo.SelectedItem.ToString());   //26 - Bases do SQL descobertas no computador local.
            dados.Add(txtBServMicro.Text);                      //27 - Tempo do micro terminal
            dados.Add(txtTimeoutQuery.Text);                    //28 - Timeou dos Querys do SQL
            dados.Add(chBoxLog.CheckState.ToString());          //29 - Habilitar ou desabilitar os logs
            dados.Add(chBoxFiltros.CheckState.ToString());      //30 - Habilitar ou desabilitar os filtros dinâmicos

            return dados;
        }

        private void BTSalvar(object sender, EventArgs e)
        {
            List<string> dados = Configs();

            /*string exibir = "";
            for (int i = 0; i < dados.Count; i++)
            {
                exibir += i.ToString() + " - " + dados[i] + "\n";
            }
            MessageBox.Show(exibir);*/

            reg.Salvar(dados);
            preencheDados();
        }

        public void BTLimparLogs(object sender, EventArgs e)
        {

            string pathZip = pRaff + "\\Logs.Zip";
            string pathArquivos = @pRaff + "\\log";

            if (Directory.GetDirectories(pathArquivos).Length.Equals(0)
            && Directory.GetFiles(pathArquivos).Length.Equals(0))
            {
                MessageBox.Show("A pasta de logs está vazia.");
            }
            else
            {
                bProgresso f = new bProgresso(pathZip, pathArquivos);
                f.Show();
            }

        }

        public void AboutBox(object sender, EventArgs e)
        {
            Forms.AboutBox a = new Forms.AboutBox();
            a.Show();
        }

        private void Bt_Integrador(object sender, EventArgs e)
        {
            if (PegarProcesso("Integrador"))
            {
                PararPrograma("Integrador");
                btIntegrador.Text = "Iniciar";
            }
            else
            {
                IniciarPrograma(txtIntegrador.Text);
                btIntegrador.Text = "Parar";
            }
            Monitorador();
        }

        private void Bt_Quantum(object sender, EventArgs e)
        {
            if (PegarProcesso("QS"))
            {
                PararPrograma("QS");
                btQuantum.Text = "Iniciar";
            }
            else
            {
                IniciarPrograma(txtQuantum.Text);
                btQuantum.Text = "Parar";
            }
            Monitorador();
        }

        private void Bt_Replicador(object sender, EventArgs e)
        {
            if (PegarProcesso("Replicador"))
            {
                PararPrograma("Replicador");
                btReplicador.Text = "Iniciar";
            }
            else
            {
                IniciarPrograma(txtReplicador.Text);
                btReplicador.Text = "Parar";
            }
            Monitorador();
        }

        private void Bt_Servico(object sender, EventArgs e)
        {
            if (PegarProcesso("Servico"))
            {
                PararPrograma("Servico");
                btServico.Text = "Iniciar";
            }
            else
            {
                IniciarPrograma(txtServico.Text);
                btServico.Text = "Parar";
            }
            Monitorador();
        }

        private void Bt_Servidor(object sender, EventArgs e)
        {
            if (PegarProcesso("Servidor"))
            {
                PararPrograma("Servidor");
                btServidor.Text = "Iniciar";
            }
            else
            {
                IniciarPrograma(txtServidor.Text);
                btServidor.Text = "Parar";
            }
            Monitorador();
        }

        private void Bt_Sincronizador(object sender, EventArgs e)
        {
            if (PegarProcesso("Sincronizador"))
            {
                PararPrograma("Sincronizador");
                btSincronizador.Text = "Iniciar";
            }
            else
            {
                IniciarPrograma(txtSincronizador.Text);
                btSincronizador.Text = "Parar";
            }
            Monitorador();
        }

        private void BtAtualizarStatus(object sender, EventArgs e)
        {
            Monitorador();
        }
    }
}
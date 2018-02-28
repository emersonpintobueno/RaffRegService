using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Microsoft.Win32;
using Microsoft.SqlServer;
using System.Data;
using System.Data.Sql;
using System.ServiceProcess;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace RaffRegServer.Classes
{
    public class registro {

        string lh = "localhost";

        public string[][] leRegistro() {

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
            cp1 "Host"=lh
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
            se1 "NomeHost"=lh
            se2 "Porta"=dword:00002711
            se3 "Host"=lh

            [HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Raffinato 2009\Sincronizador]
            si1 "Porta"=dword:00002774
            si2 "Host"=lh
            */
            #endregion

            string[][] vlr = new string[31][];
            
            //string registryValue = string.Empty;
            RegistryKey localKey = null;
            if (Environment.Is64BitOperatingSystem)
            {
                localKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64)
                    .OpenSubKey(@"SOFTWARE\\Wow6432Node\\Raffinato 2009\\");
            }
            else
            {
                localKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry32)
                .OpenSubKey(@"SOFTWARE\\Raffinato 2009\\");
            }

            //Conexão
            //PDV Host
            try
            {
                vlr[0] = new string[2] { "Host(PDV)", localKey.OpenSubKey(@"Conexão").GetValue("Host").ToString() }; //PDV Host
            }
            catch (NullReferenceException)
            {
                vlr[0] = new string[2] { "Host(PDV)", lh }; 
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao recuperar o valor do Host(PDV)", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Porta do PDV
            try
            {
                vlr[1] = new string[2] { "Porta(PDV)", localKey.OpenSubKey(@"Conexão").GetValue("Porta").ToString() };
            }
            catch (NullReferenceException)
            {
                vlr[1] = new string[2] { "Porta(PDV)", "10000" }; 
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao recuperar o valor da Porta(PDV)", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Filial
            try
            {
                vlr[2] = new string[2] { "Filial", localKey.OpenSubKey(@"Conexão").GetValue("Filial").ToString() };
            }
            catch (NullReferenceException)
            {
                vlr[2] = new string[2] { "Filial", "1" };
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao recuperar o valor da Filial", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //IdImagem
            try
            {
                vlr[3] = new string[2] { "Filial Imagem", localKey.OpenSubKey(@"Conexão").GetValue("IdImagem").ToString() };
            }
            catch (NullReferenceException)
            {
                vlr[3] = new string[2] { "Filial Imagem", "1"};
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao recuperar os valores das Imagens.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Host da Matriz
            try
            {
                vlr[4] = new string[2] { "HostMatriz", localKey.OpenSubKey(@"Conexão").GetValue("HostMatriz").ToString() };
            }
            catch (NullReferenceException)
            {
                vlr[4] = new string[2] { "HostMatriz", lh };
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao recuperar os valor do Host da Matriz.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Porta Matriz
            try
            {
                vlr[5] = new string[2] { "HostMatriz Porta", localKey.OpenSubKey(@"Conexão").GetValue("PortaMatriz").ToString() };
            }
            catch (NullReferenceException)
            {
                vlr[5] = new string[2] { "HostMatriz Porta", "10000"};
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao recuperar os valor da Porta da Matriz.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Modo Touch
            try
            {
                vlr[6] = new string[2] { "ModoTouch", localKey.OpenSubKey(@"Conexão").GetValue("ModoTouch").ToString() };
            }
            catch (NullReferenceException)
            {
                vlr[6] = new string[2] { "ModoTouch", "0"};
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao recuperar os valor do Modo Touch.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //MenuWeb
            try
            {
                vlr[7] = new string[2] { "MenuWeb", localKey.OpenSubKey(@"Conexão").GetValue("MenuWeb").ToString() };
            }
            catch (NullReferenceException)
            {
                vlr[7] = new string[2] { "MenuWeb", "1" };
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao recuperar os valor do MenuWeb.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            //Sincronizador
            //Host
            try
            {
                vlr[8] = new string[2] { "Host", localKey.OpenSubKey(@"Sincronizador").GetValue("Host").ToString() };
            }
            catch (NullReferenceException)
            {
                vlr[8] = new string[2] { "Host", lh };
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao recuperar os valor do Host do Sincronizador.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Porta Sincronizador
            try
            {
                vlr[9] = new string[2] { "Porta", localKey.OpenSubKey(@"Sincronizador").GetValue("Porta").ToString() };
            }
            catch (NullReferenceException)
            {
                vlr[9] = new string[2] { "Porta", "10100" };
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao recuperar os valor da Porta do Sincronizador.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Host Replicador
            try
            {
                vlr[10] = new string[2] { "Host", localKey.OpenSubKey(@"Sincronizador").GetValue("Host").ToString() };
            }
            catch (NullReferenceException)
            {
                vlr[10] = new string[2] { "Host", lh };
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao recuperar os valor do Host do Replicador.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Porta do Replicador
            try
            {
                vlr[11] = new string[2] { "Porta", localKey.OpenSubKey(@"Sincronizador").GetValue("Porta").ToString() };
            }
            catch (NullReferenceException)
            {
                vlr[11] = new string[2] { "Porta", "10000" };
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao recuperar os valor da Porta do Replicador.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Paff
            //Porta do PDV
            try
            {
                vlr[12] = new string[2] { "Porta PDV", localKey.OpenSubKey(@"PAF").GetValue("Porta").ToString() };
            }
            catch (NullReferenceException)
            {
                vlr[12] = new string[2] { "Porta PDV", "10002" };
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao recuperar os valor da Porta do PDV.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            //Serviço
            //Host do Serviço
            try
            {
                vlr[13] = new string[2] { "Host", localKey.OpenSubKey(@"Serviço").GetValue("Host").ToString() };
            }
            catch (NullReferenceException)
            {
                vlr[13] = new string[2] { "Host", lh };
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao recuperar os valor do Host do Serviço.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Porta do Serviço
            try
            {
                vlr[14] = new string[2] { "Nome da Porta", localKey.OpenSubKey(@"Serviço").GetValue("Porta").ToString() };
            }
            catch (NullReferenceException)
            {
                vlr[14] = new string[2] { "Nome da Porta", "10001" };
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao recuperar os valor da Porta do Serviço.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            //Tablet
            //Host Conexão do PDA
            try
            {
                vlr[15] = new string[2] { "Host", localKey.OpenSubKey(@"Conexão PDA").GetValue("Host").ToString() };
            }
            catch (NullReferenceException)
            {
                vlr[15] = new string[2] { "Host", lh };
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao recuperar os valor da Porta do Serviço.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Porta da Conexão do PDA
            try
            {
                vlr[16] = new string[2] { "Porta", localKey.OpenSubKey(@"Conexão PDA").GetValue("Porta").ToString() };
            }
            catch (NullReferenceException)
            {
                vlr[16] = new string[2] { "Porta", "10050" };
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao recuperar os valor da Porta do Serviço.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            //Modulos
            //Coleta todos os Módulos
            try
            {
                vlr[17] = new string[2] { "Coleta os modulos", localKey.OpenSubKey(@"Conexão").GetValue("Modulos").ToString() };
            }
            catch (NullReferenceException)
            {
                vlr[17] = new string[2] { "Coleta os modulos", "1;2;" };
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao recuperar os Módulos usados.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            //SQL
            //Drive SQL
            try
            {
                vlr[18] = new string[2] { "Driver SQL", localKey.OpenSubKey(@"Servidor").GetValue("DriverSQL").ToString() };
            }
            catch (NullReferenceException)
            {
                vlr[18] = new string[2] { "Driver SQL", "MSSQL$SQL2014" };
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao recuperar o nome do Serviço do SQL.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            //Host do SQL
            try
            {
                vlr[19] = new string[2] { "Host do Servidor", localKey.OpenSubKey(@"Servidor").GetValue("Host").ToString() };
            }
            catch (NullReferenceException)
            {
                vlr[19] = new string[2] { "Host do Servidor", "NomeInstSQL" };
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao recuperar o nome da instância do SQL.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            //Porta do SQL
            try
            {
                vlr[20] = new string[2] { "Nome da Base", localKey.OpenSubKey(@"Servidor").GetValue("NomeDataBase").ToString() };
            }
            catch (NullReferenceException)
            {
                vlr[20] = new string[2] { "Nome da Base", "Raffinato2009" };
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao recuperar o nome da instância do SQL.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            //Microterminal
            try
            {
                vlr[21] = new string[2] { "Tempo Limite do Microterminal", localKey.OpenSubKey(@"Servidor")
                    .GetValue("TempoLimiteMicroTerminal").ToString() };
            }
            catch (NullReferenceException)
            {
                vlr[21] = new string[2] { "Tempo Limite do Microterminal", "3"};
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao recuperar o tempo do Micro Terminal.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

            //Query do SQL
            try
            {
                vlr[22] = new string[2] { "Timeout das Querys", localKey.OpenSubKey(@"Servidor")
                    .GetValue("TimeOutQuery").ToString() };
            }
            catch (NullReferenceException)
            {
                vlr[22] = new string[2] { "Timeout das Querys", "180" };
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao recuperar o valor do Timeout das Querys","Erro!",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

            //Ativar log do servidor
            try
            {
                vlr[23] = new string[2] { "Logs do Servidor", localKey.OpenSubKey(@"Servidor")
                    .GetValue("GerarLog").ToString() };
            }
            catch (NullReferenceException)
            {
                vlr[23] = new string[2] { "Logs do Servidor", "1" };
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao recuperar o valor dos Logs", "Erro!", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            //Ativar Filtros
            try
            {
                vlr[24] = new string[2] { "Ativar filtros", localKey.OpenSubKey(@"Servidor")
                    .GetValue("UsarFiltro").ToString() };
            }
            catch (NullReferenceException)
            {
                vlr[24] = new string[2] { "Ativar filtros", "1" };
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao recuperar a definição de Ativação dos Filtros.", "Erro!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

            return vlr;
        }

        public static void DisplayIPAddresses()
        {
            StringBuilder sb = new StringBuilder();

            // Get a list of all network interfaces (usually one per network card, dialup, and VPN connection) 
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface network in networkInterfaces)
            {
                // Read the IP configuration for each network 
                IPInterfaceProperties properties = network.GetIPProperties();

                // Each network interface may have multiple IP addresses 
                foreach (IPAddressInformation address in properties.UnicastAddresses)
                {
                    // We're only interested in IPv4 addresses for now 
                    if (address.Address.AddressFamily != AddressFamily.InterNetwork)
                        continue;

                    // Ignore loopback addresses (e.g., 127.0.0.1) 
                    if (IPAddress.IsLoopback(address.Address))
                        continue;

                    sb.AppendLine(address.Address.ToString() + " (" + network.Name + ")");
                }
            }

            MessageBox.Show(sb.ToString());
        }

        public List<string> CIPs()
        {
            List<string> pIPS = new List<string>();
            // Pegar nome da maquina
            String strHostName = Dns.GetHostName();
            pIPS.Add(lh);
            pIPS.Add(strHostName);

            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            // Pegar IP's
            foreach (NetworkInterface network in networkInterfaces)
            {
                // Ler os ip's das interfaces de rede
                IPInterfaceProperties properties = network.GetIPProperties();

                // Cada interface pode ter múltiplos IP's
                foreach (IPAddressInformation address in properties.UnicastAddresses)
                {
                    // Pegar apenas o IPV4
                    if (address.Address.AddressFamily != AddressFamily.InterNetwork)
                        continue;
                    if (address.Address.ToString().Substring(0,3).Equals("169"))
                        continue;

                    // Ignorar loopback (127.0.0.1) 
                    //if (IPAddress.IsLoopback(address.Address))
                        //continue;
                    pIPS.Add(address.Address.ToString());
                    //sb.AppendLine(address.Address.ToString() + " (" + network.Name + ")");
                }
            }
            return pIPS;
        }

        public void SalvarArquivoConfiguracoes(List<string> dados)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktop, "Configurações.txt");
            if (File.Exists(filePath))
            {
                DialogResult r = MessageBox.Show("O arquivo que você deseja gravar já existe. Apagar?",
                "Important Question",
                MessageBoxButtons.YesNo);

                if (r == DialogResult.Yes)
                {
                    try
                    {
                        File.Delete(filePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Não foi possível apagar o arquivo.\n" +
                            "O erro retornado foi:\n" +
                            ex + "\n\n" +
                            "Por favor apague o arquivo" +
                            "manualmente.", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw;
                    }

                }
                else
                {
                    MessageBox.Show("Os dados serão acrescentados ao arquivo existente");
                }
            }
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, true))
                {
                    var n = Environment.NewLine;
                    file.WriteLine("#1 - Endereço do Serviço: "+ dados[0] + n);
                    file.WriteLine("#2 - Porta do Serviço: "+ dados[1] + n);
                    file.WriteLine("#3 - Porta do PAF-ECF: "+ dados[2] + n);
                    file.WriteLine("#4 - Endereço do Sincronizador: "+ dados[3] + n);
                    file.WriteLine("#5 - Porta do Sincronizador: "+ dados[4] + n);
                    file.WriteLine("#6 - Endereço do Replicador: "+ dados[5] + n);
                    file.WriteLine("#7 - Porta do Replicador: "+ dados[6] + n);
                    file.WriteLine("#8 - Endereço do Host do PDV: "+ dados[7] + n);
                    file.WriteLine("#9 - Porta do Host do PDV: "+ dados[8] + n);
                    file.WriteLine("#10 - Endereço do Host do Retaguarda: "+ dados[9] + n);
                    file.WriteLine("#11 - Porta do Host do Retaguarda: "+ dados[10] + n);
                    file.WriteLine("#12 - Filial: "+ dados[11] + n);
                    file.WriteLine("#13 - Imagens da Filial: "+ dados[12] + n);
                    file.WriteLine("#14 - Utilização do Modo Touch (verdadeiro sim, falso não): "+ dados[13] + n);
                    file.WriteLine("#15 - Utilização do menu web: "+ dados[14] + n);
                    file.WriteLine("#16 - Endereço para conexão dos tablets: "+ dados[15] + n);
                    file.WriteLine("#17 - Porta para conexão dos tablets: "+ dados[16] + n);
                    file.WriteLine("#18 - Habilitar módulo Venda Rápida: "+ dados[17] + n);
                    file.WriteLine("#19 - Habilitar módulo Venda Mesa: "+ dados[18] + n);
                    file.WriteLine("#20 - Habilitar módulo Tele-Entrega: "+ dados[19] + n);
                    file.WriteLine("#21 - Habilitar módulo Cartão Consumo: "+ dados[20] + n);
                    file.WriteLine("#22 - Host do Servidor do SQL (lido do registro): "+ dados[21] + n);
                    file.WriteLine("#23 - Porta do Servidor SQL (lido do registro): "+ dados[22] + n);
                    file.WriteLine("#24 - Servidor -Host da máquina- (lido da máquina): "+ dados[23] + n);
                    file.WriteLine("#25 - Instâncias de SQL descobertas no computador local: "+ dados[24] + n);
                    file.WriteLine("#26 - Nome do serviço do SQL descoberto no computador local: "+ dados[25] + n);
                    file.WriteLine("#27 - Bases do SQL descobertas no computador local: "+ dados[26] + n);
                    file.WriteLine("#28 - Tempo do micro terminal: "+ dados[27] + n);
                    file.WriteLine("#29 - Timeou dos Querys do SQL: "+ dados[28] + n);
                    file.WriteLine("#30 - Habilitar ou desabilitar os logs: "+ dados[29] + n);
                    file.WriteLine("#31 - Habilitar ou desabilitar os filtros dinâmicos: "+ dados[30] + n);

                    MessageBox.Show("Arquivo gravado com sucesso na pasta:\n" + filePath.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao tentar escrever o arquivo.\nO erro foi:\n" + ex, 
                    "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            

        }

        public void SalvarRegistro(RegistryKey chave, string pR, string tit, string vlr, RegistryValueKind tip)
        {

            //localKey.OpenSubKey(@"Serviço", true).SetValue("Host", dados[0]);
            //localKey.OpenSubKey(@"Serviço", true).SetValue("Porta", dados[1], RegistryValueKind.DWord);
            if (chave.OpenSubKey(pR) == null)
            {
                chave.CreateSubKey(pR);
                //MessageBox.Show("Não existe" + chave.OpenSubKey(pR));
            }
            try
            {
                chave.OpenSubKey(pR, true).SetValue(tit, vlr, tip);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao gravar as alterações de registro.\nO erro retornado foi:\n\n" + ex, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw;
            }
        }

        public void SalvarRegistro(RegistryKey chave, string pR, string tit, string vlr)
        {
            if (chave.OpenSubKey(pR) == null)
            {
                chave.CreateSubKey(pR);
                //MessageBox.Show("Não existe" + chave.OpenSubKey(pR));
            }
            try
            {
                chave.OpenSubKey(pR, true).SetValue(tit, vlr);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao gravar as alterações de registro.\nO erro retornado foi:\n\n" + ex, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw;
            }
        }

        public void Salvar(List<string> dados)
        {
            RegistryKey localKey = null;
            if (Environment.Is64BitOperatingSystem)
            {
                localKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64)
                    .OpenSubKey(@"SOFTWARE\\Wow6432Node\\Raffinato 2009\\", true);
            }
            else
            {
                localKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry32)
                    .OpenSubKey(@"SOFTWARE\\Raffinato 2009\\", true);
            }

            try
            {
                //Serviço
                SalvarRegistro(localKey, "Serviço", "Host", dados[0]);
                SalvarRegistro(localKey, "Serviço", "Porta", dados[1], RegistryValueKind.DWord);

                //PAF-ECF (porta)
                SalvarRegistro(localKey, "PAF", "Porta", dados[2]);

                //Sincronizador
                SalvarRegistro(localKey, "Sincronizador", "Host", dados[3]);
                SalvarRegistro(localKey, "Sincronizador", "Porta", dados[4], RegistryValueKind.DWord);


                //Replicador
                SalvarRegistro(localKey, "Replicador", "Host", dados[5]);
                SalvarRegistro(localKey, "Replicador", "Porta", dados[6], RegistryValueKind.DWord);

                //PDV
                SalvarRegistro(localKey, "Conexão", "Host", dados[7]);//Host do PDV
                SalvarRegistro(localKey, "Conexão", "Porta", dados[8], RegistryValueKind.DWord);//Porta do PDV

                //Retaguarda
                SalvarRegistro(localKey, "Conexão", "HostMatriz", dados[9]);
                SalvarRegistro(localKey, "Conexão", "PortaMatriz", dados[10], RegistryValueKind.DWord);

                //Filial e Imagens
                SalvarRegistro(localKey, "Conexão", "Filial", dados[11], RegistryValueKind.DWord);
                SalvarRegistro(localKey, "Conexão", "IdImagem", dados[12], RegistryValueKind.DWord);

                //Modo Touch
                if (dados[13] == "Checked")
                {
                    SalvarRegistro(localKey, "Conexão", "ModoTouch", "1", RegistryValueKind.DWord);
                }
                else
                {
                    SalvarRegistro(localKey, "Conexão", "ModoTouch", "0", RegistryValueKind.DWord);
                }

                //MenuWeb
                if (dados[14] == "Checked")
                {
                    SalvarRegistro(localKey, "Conexão", "MenuWeb", "1", RegistryValueKind.DWord);
                }
                else
                {
                    SalvarRegistro(localKey, "Conexão", "MenuWeb", "0", RegistryValueKind.DWord);
                }

                //Tablets
                SalvarRegistro(localKey, "Conexão PDA", "Host", dados[15]);
                SalvarRegistro(localKey, "Conexão PDA", "Porta", dados[16], RegistryValueKind.DWord);

                //Modulos ativos
                string m = "";
                //1 = Venda rápida
                if (dados[17] == "Checked")
                {
                    m += "1;";
                }
                //2 = Venda tele entrega
                if (dados[19] == "Checked")
                {
                    m += "2;";
                }
                //3 = Venda mesa
                if (dados[18] == "Checked")
                {
                    m += "3;";
                }
                //4 = Venda cartão consumo
                if (dados[20] == "Checked")
                {
                    m += "4;";
                }

                //Módulos Venda Rápida/Mesa/Tele-Entrega/Cartão Consumo
                SalvarRegistro(localKey, "Conexão", "Modulos", m);

                //Host do Sql (Servidor + Instância
                string d = dados[23] + "\\" + dados[24];
                SalvarRegistro(localKey, "Servidor", "Host", d);

                //Serviço do SQL
                SalvarRegistro(localKey, "Servidor", "NomeServicoSQL", dados[25]);

                //Base do Raffinat
                SalvarRegistro(localKey, "Servidor", "NomeDataBase", dados[26]);

                //Tempo do Micro Terminal
                SalvarRegistro(localKey, "Servidor", "TempoLimiteMicroTerminal", dados[27]);

                //Timeout das Querys do SQL
                SalvarRegistro(localKey, "Servidor", "TimeOutQuery", dados[28], RegistryValueKind.DWord);

                //Ativar/Desativar Geração de Logs
                if (dados[29] == "Checked")
                {
                    SalvarRegistro(localKey, "Servidor", "GerarLog", "1", RegistryValueKind.DWord);
                }
                else
                {
                    SalvarRegistro(localKey, "Servidor", "GerarLog", "0", RegistryValueKind.DWord);
                }

                //Habilitar/Desabilitar Filtros
                if (dados[30] == "Checked")
                {
                    SalvarRegistro(localKey, "Servidor", "UsarFiltro", "1", RegistryValueKind.DWord);
                }
                else
                {
                    SalvarRegistro(localKey, "Servidor", "UsarFiltro", "0", RegistryValueKind.DWord);
                }

                MessageBox.Show("Alterações Salvas", "Salvo!", MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao gravar as alterações de registro.\nO erro retornado foi:\n\n" + ex, "Erro", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw;
            }
        }

        public ServiceController SC(string servico)
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

    }
}

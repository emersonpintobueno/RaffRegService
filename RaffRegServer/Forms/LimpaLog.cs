using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Threading;
using Ionic.Zip;

namespace ZipProgress
{
    public partial class bProgresso : Form
    {
        string pathZip, pathArquivos;
        public bProgresso(string pathZip, string pathArquivos)
        {
            this.pathZip = pathZip;
            this.pathArquivos = pathArquivos;
            InitializeComponent();
        }

        public void SaveProgress(object sender, SaveProgressEventArgs e)
        {
            if (e.EventType == ZipProgressEventType.Saving_Started)
            {
                lblArquivo.Invoke((MethodInvoker)(() => lblArquivo.Visible = true));
                lblArquivo.Invoke((MethodInvoker)(() => lblArquivo.Text = "Destino: "));
                lblArquivoLe.Invoke((MethodInvoker)(() => lblArquivoLe.Visible = true));
                lblArquivoLe.Invoke((MethodInvoker)(() => lblArquivoLe.Text = "Preparando Arquivo.Zip"));
                lblTotal.Invoke((MethodInvoker)(() => lblTotal.Visible = true));
                lblComprimindo.Invoke((MethodInvoker)(() => lblComprimindo.Visible = true));
                lblComprimindo.Invoke((MethodInvoker)(() => lblComprimindo.Text = "Arquivo: "));
                lblComprimindoLe.Invoke((MethodInvoker)(() => lblComprimindoLe.Visible = true));
                lblComprimindoLe.Invoke((MethodInvoker)(() => lblComprimindoLe.Text = "Arquivo Atual..."));
                lblTotal.Invoke((MethodInvoker)(() => lblTotal.Text = "(0/0)"));
            }

            else if (e.EventType == ZipProgressEventType.Saving_BeforeWriteEntry)
            {
                lblComprimindoLe.Invoke((MethodInvoker)(() => lblComprimindoLe.Text = e.CurrentEntry.LocalFileName));
                lblArquivoLe.Invoke((MethodInvoker)(() => lblArquivoLe.Text = e.ArchiveName));
                lblTotal.Invoke((MethodInvoker)(() => lblTotal.Text = " (" + (e.EntriesSaved + 1) + "/" + e.EntriesTotal + ")"));

                progressBar2.Invoke((MethodInvoker)(() => progressBar2.Maximum = e.EntriesTotal));
                progressBar2.Invoke((MethodInvoker)(() => progressBar2.Value = e.EntriesSaved + 1));
            }
            else if (e.EventType == ZipProgressEventType.Saving_EntryBytesRead)
            {
                progressBar1.Invoke((MethodInvoker)(() => progressBar1.Value = (int)((e.BytesTransferred * 100) / e.TotalBytesToTransfer)));
                //progressBar1.Value = (int)((e.BytesTransferred * 100) / e.TotalBytesToTransfer);
            }
            else if (e.EventType == ZipProgressEventType.Saving_Completed)
            {
                Invoke((MethodInvoker)(() => this.Close()));
            }
        }

        private void Fechar(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtZipar_Apagar(object sender, EventArgs e)
        {
            Thread t = new Thread(Zipar_Apagar);
            t.Start();
        }

        private void BtApagar(object sender, EventArgs e)
        {
            Exception er = ApagarArquivosLog(pathArquivos);

            if (er == null)
            {
                MessageBox.Show("Arquivos de log apagados. Pasta limpa.", "Arquivos Apagados!", MessageBoxButtons.OK);
            }
            else
            {
                DialogResult resultado = MessageBox.Show("Ocorreram erros ao apagar os arquivos." +
                            "\nGostaria de ver esses erros?", "Important Question",
                            MessageBoxButtons.YesNo);

                if (resultado == DialogResult.Yes)
                {
                    string mr = "O seguinte erro foi reportado.\n";
                    mr += "------------------------------\n";
                    MessageBox.Show(mr + er.ToString(), "Erro ao apagar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private Exception ApagarArquivosLog(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            Exception e = null;
            try
            {
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
            }
            catch (Exception ex)
            {
                e = ex;
            }
            return e;
        }

        private void Zipar_Apagar()
        {
            using (ZipFile zip = new ZipFile())
            {
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                zip.SaveProgress += SaveProgress;
                zip.AddDirectory(pathArquivos); // vai pegar sub-diretorios
                zip.Save(pathZip);

                string msg = "Arquivo de backup gerado com sucesso:\n";
                msg += pathZip;
                Exception e = ApagarArquivosLog(pathArquivos);

                if (e == null)
                {
                    MessageBox.Show(msg, "Arquivo Gravado!", MessageBoxButtons.OK);
                }
                else
                {
                    DialogResult resultado = MessageBox.Show("O arquivo \n" + pathZip +
                        "\nFoi gravado com sucesso. \nEntretanto erros ao apagar foram reportados." +
                        "\nGostaria de ver esses erros?","Important Question",
                        MessageBoxButtons.YesNo);

                    if (resultado == DialogResult.Yes)
                    {
                        string er = "O seguinte erro foi reportado.\n";
                        er += "------------------------------\n";
                        MessageBox.Show(er + e.ToString(), "Erro ao gravar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}

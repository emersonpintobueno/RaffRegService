namespace ZipProgress
{
    partial class bProgresso
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblComprimindo = new System.Windows.Forms.Label();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.lblArquivo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lblComprimindoLe = new System.Windows.Forms.Label();
            this.lblArquivoLe = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 108);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(315, 20);
            this.progressBar1.TabIndex = 1;
            // 
            // lblComprimindo
            // 
            this.lblComprimindo.AutoSize = true;
            this.lblComprimindo.Location = new System.Drawing.Point(10, 92);
            this.lblComprimindo.Name = "lblComprimindo";
            this.lblComprimindo.Size = new System.Drawing.Size(46, 13);
            this.lblComprimindo.TabIndex = 2;
            this.lblComprimindo.Text = "Arquivo:";
            this.lblComprimindo.Visible = false;
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(12, 58);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(315, 20);
            this.progressBar2.TabIndex = 3;
            // 
            // lblArquivo
            // 
            this.lblArquivo.AutoSize = true;
            this.lblArquivo.Location = new System.Drawing.Point(12, 43);
            this.lblArquivo.Name = "lblArquivo";
            this.lblArquivo.Size = new System.Drawing.Size(46, 13);
            this.lblArquivo.TabIndex = 4;
            this.lblArquivo.Text = "Destino:";
            this.lblArquivo.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(319, 26);
            this.label1.TabIndex = 4;
            this.label1.Text = "Este procedimento irá realizar a manutenção dos arquivos de logs \r\nantigos. Você " +
    "pode:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(148, 158);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Zipar e Apagar";
            this.toolTip.SetToolTip(this.button1, "Esta opção irá realizar o backup dos arquivos de log, compactando-os\r\nna pasta ra" +
        "iz do Raffinato. Na sequência será realizado a manutenção \r\nda pasta, apagando t" +
        "odos os arquivos de log.");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.BtZipar_Apagar);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(55, 158);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Apagar";
            this.toolTip.SetToolTip(this.button2, "Apagar os arquivos de logs da pasta Raffinato\\Logs.\r\nOs arquivos serão diretament" +
        "e apagados, sem geração de backups\r\nou outros alertas.\r\nCuidado.\r\n");
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.BtApagar);
            // 
            // lblComprimindoLe
            // 
            this.lblComprimindoLe.AutoEllipsis = true;
            this.lblComprimindoLe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComprimindoLe.Location = new System.Drawing.Point(62, 91);
            this.lblComprimindoLe.Name = "lblComprimindoLe";
            this.lblComprimindoLe.Size = new System.Drawing.Size(265, 15);
            this.lblComprimindoLe.TabIndex = 2;
            this.lblComprimindoLe.Text = "Escrevendo:";
            this.lblComprimindoLe.Visible = false;
            // 
            // lblArquivoLe
            // 
            this.lblArquivoLe.AutoEllipsis = true;
            this.lblArquivoLe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblArquivoLe.Location = new System.Drawing.Point(62, 42);
            this.lblArquivoLe.Name = "lblArquivoLe";
            this.lblArquivoLe.Size = new System.Drawing.Size(266, 15);
            this.lblArquivoLe.TabIndex = 4;
            this.lblArquivoLe.Text = "Arquivo Zipado";
            this.lblArquivoLe.Visible = false;
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(62, 131);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(265, 15);
            this.lblTotal.TabIndex = 2;
            this.lblTotal.Text = "Escrevendo:";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblTotal.Visible = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(241, 158);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(87, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "Fechar";
            this.toolTip.SetToolTip(this.button3, "Fecha, cancelando as operações.");
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Fechar);
            // 
            // toolTip
            // 
            this.toolTip.IsBalloon = true;
            // 
            // bProgresso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 193);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblArquivoLe);
            this.Controls.Add(this.lblArquivo);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblComprimindoLe);
            this.Controls.Add(this.lblComprimindo);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "bProgresso";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Limpando os Logs";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblComprimindo;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.Label lblArquivo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lblComprimindoLe;
        private System.Windows.Forms.Label lblArquivoLe;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ToolTip toolTip;
    }
}

